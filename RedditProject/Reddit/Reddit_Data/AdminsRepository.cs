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
    public class AdminsRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public AdminsRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("AdminsTable");
            Table.CreateIfNotExists();
        }


        public void AddMail(AdminMail newLog)
        {
            TableOperation insertOperation = TableOperation.Insert(newLog);
            Table.Execute(insertOperation);
        }


        public bool CheckIfMailExists(string mail) 
        {

            var results = from g in Table.CreateQuery<AdminMail>()
                          where g.PartitionKey == "ADMINS" && g.Mail.Equals(mail)
                          select g;

            var res= results.ToList().Count==0? false: true;

            return res;


        }

        public IQueryable<AdminMail> GetAdmins() 
        {
            var results = from g in Table.CreateQuery<AdminMail>()
                          where g.PartitionKey == "ADMINS" 
                          select g;

            return results;

        }



    }
}
