using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class User:TableEntity
    {

        public String Username { get; set; }
        public String Password { get; set; }
        public String Picture { get; set; }

        public User(String guid)
        {
            PartitionKey = "User";
            RowKey = guid;
        }
        public User() { }


    }
}
