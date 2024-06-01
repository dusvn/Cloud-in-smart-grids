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
    public class TopicRepository
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public TopicRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("TopicTable");
            Table.CreateIfNotExists();
        }
        public IQueryable<Topic> RetrievePostsByUserId(string userId)
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.UserId == userId
                          select g;
            return results;
        }


        public IQueryable<Topic> RetrieveOtherPosts(string userId)
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.UserId != userId
                          select g;
            return results;
        }

        public Topic RetrievePost(string topicId)
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.RowKey == topicId
                          select g;
            return results.First();
        }

        public Topic RetrievePostForSubscribe(string topicId,string userId)
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.RowKey == topicId && g.UserId == userId
                          select g;

            if (results.ToList().Count==0) { return null; }

            return results.First();
        }



        public Topic CheckIfPostMatch(string topicId,string userId)
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.RowKey == topicId && g.UserId==userId
                          select g;

            var res = results.ToList();

            if (res.Count == 0) { return null; }

            return results.First();
        }


        public async Task<bool> DeleteTopic(string postId) 
        {
            var results = from g in Table.CreateQuery<Topic>()
                          where g.PartitionKey == "Topic" && g.RowKey == postId
                          select g;

            TableOperation deleteOperation = TableOperation.Delete(results.First());

            await Table.ExecuteAsync(deleteOperation);

            return true;


        }



        public void AddPost(Topic newTopic)
        {
            TableOperation insertOperation = TableOperation.Insert(newTopic);
            Table.Execute(insertOperation);
        }




    }
}
