using Common.DTO;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditService.Controllers
{
    public class SubscribeController : ApiController
    {
        SubscribeRepository sr= new SubscribeRepository();
        TopicRepository topicRepository = new TopicRepository();
        UserDataRepository userRepository = new UserDataRepository();

        [HttpPost]
        [Route("subscribe/subscribeTopic")]
        public async Task<IHttpActionResult> Subscribe(string userId,string postId)
        {
            User u = await userRepository.GetUserInfo(Guid.Parse(userId));
            var a=sr.IsSubscribed(postId, u.Email);
            
           
            if (a) 
            {
                Subscribe s = sr.EntitySubscribed(postId, u.Email);
                sr.RemoveSubscription(s);
                return Ok("Done");

            }

            Subscribe sNew = new Subscribe(postId, u.Email);
            sr.AddSubscribtion(sNew);
            

            return Ok("Done");


        }


        [HttpGet]
        [Route("subscribe/getSubscribe")]
        public async Task<IHttpActionResult> GetSubscribe(string userId, string postId)
        {

            Topic exist = topicRepository.RetrievePostForSubscribe(postId,userId);
            SubscribeDTO dto = new SubscribeDTO(false,true);

            if (exist != null) { return Ok(dto); }

            User u=await userRepository.GetUserInfo(Guid.Parse(userId));

            dto.IsSubscribed = sr.IsSubscribed(postId,u.Email);
            dto.IsMyPost = false;

            return Ok(dto);


        }




    }
}
