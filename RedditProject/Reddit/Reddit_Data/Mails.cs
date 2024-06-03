using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Mails:TableEntity
    {

        public DateTime SendingDateTime { get; set; }
        public string CommentId { get; set; }
        public int NumberOfSentMails { get; set; }


        public Mails(DateTime dateTime,string id,int num) 
        {
            PartitionKey = "MAILS";
            RowKey=Guid.NewGuid().ToString();
            SendingDateTime=dateTime;
            CommentId=id;
            NumberOfSentMails=num;

        }

        public Mails() {
            
            
        
        }


    }
}
