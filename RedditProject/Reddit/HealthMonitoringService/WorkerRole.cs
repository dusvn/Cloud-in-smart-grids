using Common;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
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

            Trace.TraceInformation("HealthMonitoringService has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("HealthMonitoringService is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("HealthMonitoringService has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {

                INotification proxy;
                var binding = new NetTcpBinding();
                ChannelFactory<INotification> factory = new
                ChannelFactory<INotification>(binding, new
                EndpointAddress("net.tcp://localhost:10100/health-monitoring"));
                proxy = factory.CreateChannel();


                IRedditService proxy1;
                var binding1 = new NetTcpBinding();
                ChannelFactory<IRedditService> factory1 = new
                ChannelFactory<IRedditService>(binding1, new
                EndpointAddress("net.tcp://localhost:10110/health-monitoring"));
                proxy1 = factory1.CreateChannel();


                string notification = proxy.Message();
                string redditService = proxy1.Message();

                Trace.TraceInformation($"Working {notification}  {redditService}");
                await Task.Delay(1000);
            }
        }
    }
}
