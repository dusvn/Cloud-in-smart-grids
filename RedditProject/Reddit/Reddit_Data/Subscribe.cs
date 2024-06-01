using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Subscribe:TableEntity
    {
        

        public string PostId { get; set; }
        public string Email { get; set; }

        public Subscribe()
        {
            
        }

        public Subscribe(string postId, string email)
        {
            PostId = postId;
            Email = email;
            PartitionKey = "SubscDef";
            RowKey=Guid.NewGuid().ToString();  
        }



    }
}
