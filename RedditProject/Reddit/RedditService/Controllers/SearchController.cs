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
    public class SearchController : ApiController
    {
        TopicRepository topicRepository= new TopicRepository();
        BlobHelper blob= new BlobHelper();

        [HttpGet]
        [Route("search/getByTitle")]
        public async Task<IHttpActionResult> TitlePosts(string title)
        {

            List<Topic> all = topicRepository.RetrievePostsByTitle(title).ToList();

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
        [Route("search/getAll")]
        public async Task<IHttpActionResult> AllPosts()
        {

            List<Topic> all = topicRepository.RetrieveAllPosts().ToList();

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
        [Route("search/sortAll")]
        public async Task<IHttpActionResult> SortPosts()
        {

            List<Topic> all = topicRepository.RetrieveAllPosts().ToList();

            var myPosts = new List<PostDTO>();

            foreach (var f in all)
            {

                var bajt = await blob.DownloadText(f.RowKey, "posts");
                var text = Encoding.UTF8.GetString(bajt);

                PostDTO p = new PostDTO(f.TopicName, text, f.RowKey);
                myPosts.Add(p);


            }

            var sorted = myPosts.OrderBy(o => o.Title).ToList();



            return Ok(sorted.AsEnumerable());


        }








    }
}
