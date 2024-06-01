using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.ComponentModel.Design;

namespace Reddit_Data
{
    public class CommentRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public CommentRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("CommentTable");
            Table.CreateIfNotExists();
        }


        public IQueryable<Comment> GetCommentsByUserId(string id) 
        {

            var results = from g in Table.CreateQuery<Comment>()
                          where g.PartitionKey == "Comments" && g.UserId==id
                          select g;
            return results;


        }

        public IQueryable<Comment> GetCommentsByPostId(string pid)
        {

            var results = from g in Table.CreateQuery<Comment>()
                          where g.PartitionKey == "Comments" && g.PostId == pid
                          select g;
            return results;


        }

        public IQueryable<Comment> GetCommentsSeparateComments(string pid,string uid)
        {

            var results = from g in Table.CreateQuery<Comment>()
                          where g.PartitionKey == "Comments" && g.PostId == pid && g.UserId== uid
                          select g;
            return results;


        }


        public void AddComment(Comment newComment)
        {
            TableOperation insertOperation = TableOperation.Insert(newComment);
            Table.Execute(insertOperation);
        }

        public void RemoveComment(string commentId) 
        {
            var res = from g in Table.CreateQuery<Comment>()
                      where g.PartitionKey == "Comments" && g.RowKey==commentId
                      select g;

            TableOperation deleteOperation = TableOperation.Delete(res.First());
            Table.Execute(deleteOperation);
        }



        public bool DeleteAllComments(string postId) 
        {
            var res = from g in Table.CreateQuery<Comment>()
                      where g.PartitionKey == "Comments" && g.PostId == postId
                      select g;

            List<Comment> del = res.ToList();

            del.ForEach(async (comment) =>
            {
                TableOperation deleteOperation = TableOperation.Delete(comment);
                await Table.ExecuteAsync(deleteOperation);
            });

            return true;

        }




    }
}
