using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Health:TableEntity
    {

        public DateTime HealthDateTime { get; set; }
        public string State { get; set; }
        


        public Health(DateTime dateTime, string state)
        {
            PartitionKey = "HEALTH";
            RowKey = Guid.NewGuid().ToString();
            HealthDateTime = dateTime;
            State = state;

        }

        public Health()
        {



        }


    }
}
