using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Vote:TableEntity
    {
        public string VoteType { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public Vote() { }

        public Vote(string voteType, string userId, string postId)
        {
            VoteType = voteType;
            UserId = userId;
            PostId = postId;
            RowKey=Guid.NewGuid().ToString();
            PartitionKey = "Votes";
        }
    }
}
