using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class PostDTO
    {
        public PostDTO(string title, string text, string topicId)
        {
            Title = title;
            Text = text;
            TopicId = topicId;
        }

        public string Title { get; set; }
        public string Text { get; set; }

        public string TopicId { get; set; }


    }
}
