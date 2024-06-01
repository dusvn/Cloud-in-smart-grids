using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class SubscribeRepository
    {


        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public SubscribeRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("SubscribeTable");
            Table.CreateIfNotExists();
        }
        public IQueryable<Subscribe> RetrieveSubscriptionsByPostId(string postId)
        {
            var results = from g in Table.CreateQuery<Subscribe>()
                          where g.PartitionKey == "SubscDef" && g.PostId == postId
                          select g;
            return results;
        }

        public void AddSubscribtion(Subscribe sub)
        {
            TableOperation insertOperation = TableOperation.Insert(sub);
            Table.Execute(insertOperation);
        }


        public bool IsSubscribed(string postId, string email) 
        {
            var results = from g in Table.CreateQuery<Subscribe>()
                          where g.PartitionKey == "SubscDef" && g.PostId == postId && g.Email == email
                          select g;

            if (results.ToList().Count == 0) { return false; }

            return true;


        }

        public Subscribe EntitySubscribed(string postId, string email)
        {
            var results = from g in Table.CreateQuery<Subscribe>()
                          where g.PartitionKey == "SubscDef" && g.PostId == postId && g.Email == email
                          select g;

            if (results.ToList().Count == 0) { return null; }

            return results.First();


        }


        public void RemoveSubscription(Subscribe sub)
        {
            TableOperation deleteOperation = TableOperation.Delete(sub);
            Table.Execute(deleteOperation);
        }







    }
}
