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
    public class UserDataRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public UserDataRepository()
        {
            _storageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(_storageAccount.TableEndpoint.AbsoluteUri), _storageAccount.Credentials);
            _table = tableClient.GetTableReference("UserTable");
            _table.CreateIfNotExists();
        }
        public IQueryable<User> RetrieveAllStudents()
        {
            var results = from g in _table.CreateQuery<User>()
                          where g.PartitionKey == "User"
                          select g;
            return results;
        }
        public void AddStudent(User newUser)
        {
            TableOperation insertOperation = TableOperation.Insert(newUser);
            _table.Execute(insertOperation);
        }




    }
}
