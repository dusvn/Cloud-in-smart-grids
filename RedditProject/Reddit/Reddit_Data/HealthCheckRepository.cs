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
    public class HealthCheckRepository
    {

        private CloudStorageAccount _storageAccount;
        private CloudTable _table;

        public CloudStorageAccount StorageAccount { get => _storageAccount; set => _storageAccount = value; }
        public CloudTable Table { get => _table; set => _table = value; }

        public HealthCheckRepository()
        {
            StorageAccount =
           CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudTableClient tableClient = new CloudTableClient(new
           Uri(StorageAccount.TableEndpoint.AbsoluteUri), StorageAccount.Credentials);
            Table = tableClient.GetTableReference("HealthCheckTable");
            Table.CreateIfNotExists();
        }


        public void AddHealth(Health newLog)
        {
            TableOperation insertOperation = TableOperation.Insert(newLog);
            Table.Execute(insertOperation);
        }


        

        public float Get24HourPercent()
        {
            var now = DateTime.Now;
            var hourAgo = now.AddHours(-24);

            var results = from g in Table.CreateQuery<Health>()
                          where g.PartitionKey == "HEALTH" && hourAgo <= g.HealthDateTime && g.HealthDateTime < now
                          select g;

            var all = (float)results.ToList().Count;
            var ok = (float)results.ToList().Where(n => n.State.Equals("OK")).ToList().Count;

            float percent = ok / all;


            return percent;

        }



        public List<Health> GetLastHour() 
        {
            var now=DateTime.Now;
            var hourAgo = now.AddHours(-1);

            var results = from g in Table.CreateQuery<Health>()
                          where g.PartitionKey == "HEALTH" && hourAgo<=g.HealthDateTime && g.HealthDateTime<now
                          select g;

            if (results.ToList().Count == 0)
            {
                return new List<Health>();
            }

            else 
            {
                return results.ToList();

            }

            

        }



    }
}
