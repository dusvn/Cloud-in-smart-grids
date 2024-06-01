using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class FullPostDTO
    {

        public FullPostDTO(string title, string text, string topicId,string image)
        {
            Title = title;
            Text = text;
            TopicId = topicId;
            Image = image;
        }

        public string Title { get; set; }
        public string Text { get; set; }

        public string TopicId { get; set; }

        public string Image { get; set; }


    }
}
