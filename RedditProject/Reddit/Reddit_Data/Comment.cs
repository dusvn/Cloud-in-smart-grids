using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class Comment:TableEntity
    {
        public Comment(string userId, string postId, string commentContent,Guid commId)
        {
            UserId = userId;
            PostId = postId;
            CommentContent = commentContent;
            PartitionKey = "Comments";
            RowKey= commId.ToString();
        }
        public Comment()
        {
            
        }

        public string UserId { get; set; }
        public string PostId { get; set; }

        public string CommentContent { get; set; }



    }
}
