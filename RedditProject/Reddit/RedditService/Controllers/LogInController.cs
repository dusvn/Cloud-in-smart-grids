using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;
using Reddit_Data;
using Microsoft.Azure;

namespace RedditService.Controllers
{

    [Route("api/login")]
    public class LogInController : ApiController
    {

        UserDataRepository repo = new UserDataRepository();


        [HttpPost]
        [ActionName("PostLogin")]
        public async Task<HttpResponseMessage> PostLogin() {

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var v = await Request.Content.ReadAsMultipartAsync();

            var username = await v.Contents[0].ReadAsStringAsync();
            var password = await v.Contents[1].ReadAsStringAsync();
            var image = await v.Contents[2].ReadAsStreamAsync();


            string uniqueBlobName = string.Format("image_{0}", username);
            var storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference("vezba");
            CloudBlockBlob blob = container.GetBlockBlobReference(uniqueBlobName);
            // postavljanje odabrane datoteke (slike) u blob servis koristeci blob klijent
            blob.UploadFromStream(image);

            User user = new User(username) {

                Username = username,
                Password = password,
                Picture = blob.Uri.ToString()
            
            };

            repo.AddStudent(user);

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><table><tr><th>Index</th><th>Name</th><th>LastName</th><th>Photo</th><th></th></tr>");
            sb.Append($"<tr><td>{user.Username}</td><td>{user.Password}</td>");
            sb.Append($"<td><img src=\"{user.Picture}\" /></td></tr></table></html>");


            HttpResponseMessage h1 = new HttpResponseMessage(HttpStatusCode.OK) {
            
                Content= new StringContent(sb.ToString(),Encoding.UTF8,"text/html")
            
            };

            return h1;



        }


    }


}

