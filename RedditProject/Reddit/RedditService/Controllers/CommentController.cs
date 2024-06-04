using Common.DTO;
using Microsoft.WindowsAzure.Storage.Queue;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditService.Controllers
{
    public class CommentController : ApiController
    {
        private CommentRepository coRepo= new CommentRepository();
        private TopicRepository topicRepository = new TopicRepository();
        private UserDataRepository repo = new UserDataRepository();
        private BlobHelper blb= new BlobHelper();

        [HttpPost]
        [Route("comments/addComment")]
        public async Task<IHttpActionResult> AddComment()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var topicId = await provider.Contents[0].ReadAsStringAsync();
            var userId = await provider.Contents[1].ReadAsStringAsync();
            var comment = await provider.Contents[2].ReadAsStringAsync();

            var bajtCom = Encoding.UTF8.GetBytes(comment);

            var idx = Guid.NewGuid();

            var tbl=await blb.UploadComment(bajtCom,idx);

            Comment c = new Comment(userId,topicId,tbl,idx);

            coRepo.AddComment(c);



            CloudQueue queRef = QueueHelper.GetQueueReference("notifications");
            byte[] contetn = Encoding.ASCII.GetBytes(c.RowKey);
            CloudQueueMessage message = new CloudQueueMessage(contetn);
            queRef.AddMessage(message);


            return Ok("Successfuly added new topic!");


        }


        [HttpGet]
        [Route("comments/getComments")]
        public async Task<IHttpActionResult> GetComments(string userId,string postId)
        {

            Topic t = topicRepository.CheckIfPostMatch(postId, userId);
            List<Comment> comments = coRepo.GetCommentsByPostId(postId).ToList();
            List<Comment> userComments = coRepo.GetCommentsSeparateComments(postId, userId).ToList();

            List<MyTopicCommentsDTO> dtos=new List<MyTopicCommentsDTO>();

            if (comments.Count == 0) { return Ok(dtos); }


            if (t != null)
            {

                dtos = await MappingComments(comments, true);

                return Ok(dtos.AsEnumerable());


            }


            if (userComments.Count == 0) 
            {

                dtos = await MappingComments(comments, false);

                return Ok(dtos.AsEnumerable());

            }


            foreach (Comment comment in comments)
            {
                var r = await repo.GetUserInfo(Guid.Parse(comment.UserId));
                var bajt = await blb.DownloadComment(comment.RowKey, "comments");
                var text = Encoding.UTF8.GetString(bajt);

                if (comment.UserId.Equals(userId)) 
                {
                    MyTopicCommentsDTO dtoU = new MyTopicCommentsDTO(true, text, r.Email,comment.RowKey);
                    dtos.Add(dtoU);
                    continue;

                }

                MyTopicCommentsDTO dto = new MyTopicCommentsDTO(false, text, r.Email,comment.RowKey);

                dtos.Add(dto);

            }





            return Ok(dtos.AsEnumerable());

        }


        [HttpDelete]
        [Route("comments/deleteComments")]
        public async Task<IHttpActionResult> DeleteComment(string commentId)
        {

            await blb.DeleteFileFromComments(Guid.Parse(commentId));

            coRepo.RemoveComment(commentId);

            return Ok("Deleted");

        }



        public async Task<List<MyTopicCommentsDTO>> MappingComments(List<Comment> comments,bool IsEnabled)
        {

            List<MyTopicCommentsDTO> mapped = new List<MyTopicCommentsDTO>();

            if (comments.Count == 0) { return mapped; }


            foreach (Comment comment in comments) 
            {
                var r = await repo.GetUserInfo(Guid.Parse(comment.UserId));
                var bajt = await blb.DownloadComment(comment.RowKey, "comments");
                var text = Encoding.UTF8.GetString(bajt);
                MyTopicCommentsDTO dto = new MyTopicCommentsDTO(IsEnabled, text,r.Email, comment.RowKey);

                mapped.Add(dto);

            }

            return mapped;
        
        
        }






    }
}
