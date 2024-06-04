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
    public class VoteRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public VoteRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("VoteTable");
            Table.CreateIfNotExists();
        }



        public bool DidUserUpVote(string userId, string postId) 
        {
            var results = from g in Table.CreateQuery<Vote>()
                          where g.PartitionKey == "Votes" && g.PostId == postId && g.UserId == userId && g.VoteType.Equals("UP")
                          select g;



            if (results.ToList().Count > 0)
                return true;

            return false;


        }

        public bool DidUserDownVote(string userId, string postId)
        {
            var results = from g in Table.CreateQuery<Vote>()
                          where g.PartitionKey == "Votes" && g.PostId == postId && g.UserId == userId && g.VoteType.Equals("DOWN")
                          select g;



            if (results.ToList().Count > 0)
                return true;

            return false;


        }

        public int GetUpvotesForPost(string postId) 
        {
            var results = from g in Table.CreateQuery<Vote>()
                          where g.PartitionKey == "Votes" && g.PostId == postId &&  g.VoteType.Equals("UP")
                          select g;



            return results.ToList().Count;

        }

        public int GetDownvoteForPost(string postId)
        {
            var results = from g in Table.CreateQuery<Vote>()
                          where g.PartitionKey == "Votes" && g.PostId == postId && g.VoteType.Equals("DOWN")
                          select g;



            return results.ToList().Count;

        }


        public void SetUpvote(string postId, string userId)
        {
            Vote up= new Vote("UP",userId,postId);

            TableOperation insertOperation = TableOperation.Insert(up);
            Table.Execute(insertOperation);

        }

        public void SetDownvote(string postId, string userId) 
        {
            Vote up = new Vote("DOWN", userId, postId);

            TableOperation insertOperation = TableOperation.Insert(up);
            Table.Execute(insertOperation);


        }




    }
}
