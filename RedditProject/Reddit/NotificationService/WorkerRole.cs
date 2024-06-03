using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Queue;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);


        private NotificationServer notificationServer;
        public override void Run()
        {
            Trace.TraceInformation("NotificationService is running");

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

            notificationServer = new NotificationServer();
            notificationServer.Open();


            Trace.TraceInformation("NotificationService has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("NotificationService is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            notificationServer.Close();

            Trace.TraceInformation("NotificationService has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
             CommentRepository commentRepository = new CommentRepository();
             SubscribeRepository subscribeRepository = new SubscribeRepository();
             UserDataRepository userRepository = new UserDataRepository();
             TopicRepository topicRepository = new TopicRepository();
             BlobHelper blobHelper = new BlobHelper();
             SMTPMailSender smtp = new SMTPMailSender();
             SendMailsRepository mailRepository = new SendMailsRepository();
             CloudQueue storage = QueueHelper.GetQueueReference("notifications");
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {

                


                CloudQueueMessage req = storage.GetMessage();

                if (req == null)
                {

                    Trace.WriteLine("No messages in queue, fetch time 5s !", "INFO");

                }

                else
                {

                    Comment c= commentRepository.GetCommentById(req.AsString);
                    Topic topic = topicRepository.RetrievePost(c.PostId);
                    User u = await userRepository.GetUserInfo(Guid.Parse(c.UserId));

                    List<Subscribe> subs=subscribeRepository.RetrieveSubscriptionsByPostId(c.PostId).ToList();

                    if (subs.Count == 0) 
                    {
                        var d = DateTime.Now;
                        Trace.WriteLine($"There is no subscription for this post ! {d} | {req.AsString} | {subs.Count}", "WARNING");
                        Mails m = new Mails(d,req.AsString,subs.Count);
                        mailRepository.AddMail(m);

                    }
                    else
                    {
                        var textByte = await blobHelper.DownloadComment(req.AsString, "comments");

                        var comment=Encoding.UTF8.GetString(textByte);

                        subs.ForEach((s) => 
                        { 
                            smtp.SendEmail(s.Email,$"A user {u.Email} comments post {topic.TopicName}",$"A user {u.Email} commented on post {topic.TopicName} :\n\n{comment}"); 
                            
                        
                        });

                        var d = DateTime.Now;
                        Trace.WriteLine($"{d} | {req.AsString} | {subs.Count}", "INFO");
                        Mails m = new Mails(d, req.AsString, subs.Count);
                        mailRepository.AddMail(m);

                    }

                    storage.DeleteMessage(req);
                }

                await Task.Delay(5000);
            }
        }
    }
}
