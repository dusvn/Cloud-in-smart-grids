using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Topic:TableEntity
    {
        public string TopicName { get; set; }
        public string TopicText { get; set; }

        public string TopicPhoto { get; set; }

        public string UserId { get; set; }
        

        public Topic(string topicName, string topicText, string topicPhoto, Guid userId,Guid topicId)
        {
            TopicName= topicName;
            TopicText= topicText;
            TopicPhoto= topicPhoto;
            RowKey= topicId.ToString();
            PartitionKey = "Topic";
            UserId = userId.ToString();
        }

        public Topic() { }



    }
}
