using Common.DTO;
using Reddit_Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedditService.Controllers
{
    public class TopicController : ApiController
    {

        private UserDataRepository repo = new UserDataRepository();
        private BlobHelper blob = new BlobHelper();
        private TopicRepository topicRepository = new TopicRepository();
        private CommentRepository commentRepository = new CommentRepository();


        [HttpPost]
        [Route("topic/newTopic")]
        public async Task<IHttpActionResult> AddNewPostForTopic()
        {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var topicName = await provider.Contents[0].ReadAsStringAsync();
            var topicText = await provider.Contents[1].ReadAsStringAsync();
            var topicPhoto = await provider.Contents[2].ReadAsStreamAsync();
            var userId = await provider.Contents[3].ReadAsStringAsync();

            var postId = Guid.NewGuid();

            byte[] imageBytes = await ConvertStreamToByteArrayAsync(topicPhoto);

            string imageUrl = await blob.UploadImagePost(imageBytes, postId);

            string textUrl= await blob.UploadText(Encoding.UTF8.GetBytes(topicText), postId);

            Topic t = new Topic(topicName, textUrl, imageUrl, Guid.Parse(userId),postId);

            topicRepository.AddPost(t);


            return Ok("Successfuly added new topic!");


        }


        [HttpGet]
        [Route("topic/getMyPosts")]
        public async Task<IHttpActionResult> GetMyPosts(string userId)
        {

            List<Topic> all = topicRepository.RetrievePostsByUserId(userId).ToList();

            var myPosts = new List<PostDTO>();

            foreach (var f in all) {

                var bajt =await blob.DownloadText(f.RowKey, "posts");
                var text = Encoding.UTF8.GetString(bajt);

                PostDTO p = new PostDTO(f.TopicName, text, f.RowKey);
                myPosts.Add(p);


            }

            return Ok(myPosts.AsEnumerable());


        }




        [HttpGet]
        [Route("topic/getOtherPosts")]
        public async Task<IHttpActionResult> GetOtherPosts(string userId)
        {

            List<Topic> all = topicRepository.RetrieveOtherPosts(userId).ToList();

            var myPosts = new List<PostDTO>();

            foreach (var f in all)
            {

                var bajt = await blob.DownloadText(f.RowKey, "posts");
                var text = Encoding.UTF8.GetString(bajt);

                PostDTO p = new PostDTO(f.TopicName, text, f.RowKey);
                myPosts.Add(p);


            }

            return Ok(myPosts.AsEnumerable());


        }



        [HttpGet]
        [Route("topic/getFullTopic")]
        public async Task<IHttpActionResult> GetFullTopic(string postId)
        {

                Topic t = topicRepository.RetrievePost(postId);

                var bajt = await blob.DownloadText(t.RowKey, "posts");
                var text = Encoding.UTF8.GetString(bajt);

                FullPostDTO p = new FullPostDTO(t.TopicName, text, t.RowKey,t.TopicPhoto);
               
                return Ok(p);


        }


        [HttpDelete]
        [Route("topic/deletePost")]
        public async Task<IHttpActionResult> DeleteTopic(string postId)
        {
            List<Comment> ls = commentRepository.GetCommentsByPostId(postId).ToList();

            ls.ForEach(async (comment) =>
            {

                await blob.DeleteFileFromComments(Guid.Parse(comment.RowKey));

            });


           var d1= commentRepository.DeleteAllComments(postId);


            var d2=await blob.DeleteFilesFromPosts(Guid.Parse(postId));

            var d3 = await topicRepository.DeleteTopic(postId);

            

            if (d1 && d2 && d3) 
            {
                return Ok("Deleted");
            }

            return InternalServerError();


        }








        private async Task<byte[]> ConvertStreamToByteArrayAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }




    }
}
