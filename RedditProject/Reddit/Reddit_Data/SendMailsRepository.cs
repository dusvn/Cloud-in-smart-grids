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
    public class SendMailsRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public SendMailsRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("SendMailsTable");
            Table.CreateIfNotExists();
        }


        public void AddMail(Mails newLog)
        {
            TableOperation insertOperation = TableOperation.Insert(newLog);
            Table.Execute(insertOperation);
        }




    }
}
