using Common;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace HealthMonitoringService
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private HMSServer hmsServer;
        public override void Run()
        {
            Trace.TraceInformation("HealthMonitoringService is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Use TLS 1.2 for Service Bus connections
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            hmsServer = new HMSServer();
            hmsServer.Open();

            Trace.TraceInformation("HealthMonitoringService has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("HealthMonitoringService is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            hmsServer.Close();

            Trace.TraceInformation("HealthMonitoringService has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            HealthCheckRepository repo = new HealthCheckRepository();
            EmailErrorSender smtp = new EmailErrorSender();
            AdminsRepository admins = new AdminsRepository();
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                INotification proxy;
                IRedditService proxy1;

                var binding = new NetTcpBinding();
                ChannelFactory<INotification> factory = new
                ChannelFactory<INotification>(binding, new
                EndpointAddress("net.tcp://localhost:10100/health-monitoring"));

                var binding1 = new NetTcpBinding();
                ChannelFactory<IRedditService> factory1 = new
                ChannelFactory<IRedditService>(binding1, new
                EndpointAddress("net.tcp://localhost:10110/health-monitoring"));


                try
                {

                    proxy = factory.CreateChannel();
                    string notification = proxy.Message();
                    Health h = new Health(DateTime.Now, "OK");
                    Trace.WriteLine($"{notification} | {h.HealthDateTime} | {h.State}", "INFO");
                    repo.AddHealth(h);


                }
                catch (Exception)
                {

                    Health h = new Health(DateTime.Now, "NOT_OK");
                    Trace.WriteLine($"Notification service is down.....| {h.HealthDateTime} | {h.State}", "ERROR");
                    List<AdminMail> mails = admins.GetAdmins().ToList();

                    if (mails.Count > 0) 
                    { 
                        mails.ForEach((k) =>
                        {

                            smtp.SendEmail(k.Mail, $"Notification service is down !", $"Notification service is down.....| {h.HealthDateTime} | {h.State}");

                        });
                    }

                    repo.AddHealth(h);
                }

                try {

                    
                    proxy1 = factory1.CreateChannel();
                    string redditService = proxy1.Message();
                    Health h = new Health(DateTime.Now, "OK");
                    Trace.WriteLine($"{redditService}| {h.HealthDateTime} | {h.State}", "INFO");
                    repo.AddHealth(h);

                } catch (Exception) 
                {

                    Health h = new Health(DateTime.Now, "NOT_OK");
                    Trace.WriteLine($"Reddit service is down.....| {h.HealthDateTime} | {h.State}", "ERROR");

                    List<AdminMail> mails = admins.GetAdmins().ToList();

                    if (mails.Count > 0)
                    {
                        mails.ForEach((k) =>
                        {

                            smtp.SendEmail(k.Mail, $"Reddit service is down !", $"Reddit service is down.....| {h.HealthDateTime} | {h.State}");

                        });
                    }


                    repo.AddHealth(h);

                }



                await Task.Delay(5000);
            }
        }
    }
}
