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

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public UserDataRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("UserTable");
            Table.CreateIfNotExists();
        }
        public IQueryable<User> RetrieveAllStudents()
        {
            var results = from g in Table.CreateQuery<User>()
                          where g.PartitionKey == "User"
                          select g;
            return results;
        }

        public async Task<User> Login(string email,string pw)
        {
            TableQuery<User> userQuery = new TableQuery<User>()
        .Where(TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email),
            TableOperators.And,
            TableQuery.GenerateFilterCondition("Password", QueryComparisons.Equal, pw)));

            TableQuerySegment<User> queryResult = await Table.ExecuteQuerySegmentedAsync(userQuery, null);

            if (queryResult.Results.Count > 0) return queryResult.Results[0];
            else return null;
        }

        public void AddUser(User newUser)
        {
            TableOperation insertOperation = TableOperation.Insert(newUser);
            Table.Execute(insertOperation);
        }

        public async Task<bool> CheckIfAlreadyExists(string email)
        {
            TableQuery<User> userQuery = new TableQuery<User>()
        .Where(TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal, email));
         

            TableQuerySegment<User> queryResult = await Table.ExecuteQuerySegmentedAsync(userQuery, null);

            if (queryResult.Results.Count > 0) return true;
            else return false;
        }

        public async Task<User> GetUserInfo(Guid userId)
        {
            TableQuery<User> userQuery = new TableQuery<User>()
        .Where(TableQuery.GenerateFilterConditionForGuid("UserId", QueryComparisons.Equal, userId));


            TableQuerySegment<User> queryResult = await Table.ExecuteQuerySegmentedAsync(userQuery, null);
            if (queryResult.Results.Count > 0) return queryResult.Results[0];
            else return null;
        }

        public async Task<User> UpdateUser(User user)
        {
            TableQuery<User> userQuery = new TableQuery<User>()
                .Where(TableQuery.GenerateFilterConditionForGuid("UserId", QueryComparisons.Equal, user.UserId));


            TableQuerySegment<User> queryResult = await Table.ExecuteQuerySegmentedAsync(userQuery, null);


            if (queryResult.Results.Count > 0)
            {
                User existingUser = queryResult.Results[0];


                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Email = user.Email;
                existingUser.PhoneNum = user.PhoneNum;
                existingUser.Password = user.Password;
                existingUser.Image = user.Image;

                TableOperation updateOperation = TableOperation.Replace(existingUser);


                await Table.ExecuteAsync(updateOperation);

                return existingUser;
            }
            else
            {
                return null;
            }
        }





    }
}
