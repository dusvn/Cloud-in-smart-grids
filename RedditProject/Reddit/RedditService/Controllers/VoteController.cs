using Common.DTO;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditService.Controllers
{
    public class VoteController : ApiController
    {
        VoteRepository voteRepo= new VoteRepository();

        [HttpGet]
        [Route("vote/getVote")]
        public IHttpActionResult GetVotes(string postId)
        {

            var up= voteRepo.GetUpvotesForPost(postId);

            var down= voteRepo.GetDownvoteForPost(postId);

            VotesDTO votes = new VotesDTO(up,down);


            return Ok(votes);

        }


        [HttpPost]
        [Route("vote/upVote")]
        public IHttpActionResult Upvote(string postId,string userId)
        {

            if (voteRepo.DidUserDownVote(userId, postId)) {

                return Ok("Alert");
            
            }

            if (voteRepo.DidUserUpVote(userId, postId))
            {

                return Ok("Double");

            }


            voteRepo.SetUpvote(postId,userId);

            return Ok("Done");

        }

        [HttpPost]
        [Route("vote/downVote")]
        public IHttpActionResult Downvote(string postId, string userId)
        {

            if (voteRepo.DidUserUpVote(userId, postId))
            {

                return Ok("Alert");

            }
            if (voteRepo.DidUserDownVote(userId, postId))
            {

                return Ok("Double");

            }

            voteRepo.SetDownvote(postId, userId);

            return Ok("Done");

        }




    }
}
