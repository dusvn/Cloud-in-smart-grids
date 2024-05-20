using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class QueueHelper
    {

        public static CloudQueue GetQueueReference(String queueName)
        {

            CloudStorageAccount storageAccou = CloudStorageAccount.
                Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

            CloudQueueClient queueClient = storageAccou.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();

            return queue;


        }


    }
}
