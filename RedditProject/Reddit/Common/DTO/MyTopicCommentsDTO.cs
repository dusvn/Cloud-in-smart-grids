using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class MyTopicCommentsDTO
    {
        public MyTopicCommentsDTO(bool enableRemove, string comment, string userId,string commentId)
        {
            EnableRemove = enableRemove;
            Comment = comment;
            UserName = userId;
            CommentId = commentId;
        }

        public bool EnableRemove { get; set; }
        public string Comment { get; set; }

        public string UserName { get; set; }
        public string CommentId { get; set; }




    }
}
