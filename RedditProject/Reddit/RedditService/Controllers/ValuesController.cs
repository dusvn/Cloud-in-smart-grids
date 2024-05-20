using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage.Queue;
using Reddit_Data;


namespace RedditService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

            CloudQueue queRef = QueueHelper.GetQueueReference("notifications");

            byte[] contetn = Encoding.ASCII.GetBytes("RowKey");
            CloudQueueMessage message = new CloudQueueMessage(contetn);
            queRef.AddMessage(message);


            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
