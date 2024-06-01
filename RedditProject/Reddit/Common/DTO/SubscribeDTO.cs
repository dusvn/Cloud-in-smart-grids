using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class SubscribeDTO
    {
        public SubscribeDTO(bool isSubscribed, bool isMyPost)
        {
            IsSubscribed = isSubscribed;
            IsMyPost = isMyPost;
            
        }

        public bool IsSubscribed { get; set; }

        public bool IsMyPost { get; set; }

        





        


    }
}
