using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class AdminMail:TableEntity
    {

        
        public string Mail { get; set; }


        public AdminMail(string email)
        {
            PartitionKey = "ADMINS";
            RowKey = Guid.NewGuid().ToString();
            Mail = email;

        }

        public AdminMail()
        {



        }



    }
}
