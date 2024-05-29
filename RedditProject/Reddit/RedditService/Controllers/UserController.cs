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
using Common.Models;
using System.Web.Http.Results;
using Common.DTO;
using System.Web.Helpers;

namespace RedditService.Controllers
{
    
    public class UserController : ApiController
    {

        UserDataRepository repo = new UserDataRepository();
        BlobHelper blob = new BlobHelper();

        public UserDataRepository Repo { get => repo; set => repo = value; }


        [HttpPost]
        [Route("user/login")]
        public async Task<IHttpActionResult> LogIn()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();
            var email = await provider.Contents[0].ReadAsStringAsync();
            var password = await provider.Contents[1].ReadAsStringAsync();

            var user = await repo.Login(email, password);

            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            var response = new
            {
                message = "Login successful",
                user = user
            };

            return Ok(response);
        }


        [HttpPost]
        [Route("user/register")]
        public async Task<IHttpActionResult> Register()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var v = await Request.Content.ReadAsMultipartAsync();

            var FirstName = await v.Contents[0].ReadAsStringAsync();
            var LastName = await v.Contents[1].ReadAsStringAsync();
            var City = await v.Contents[2].ReadAsStringAsync();
            var Address = await v.Contents[3].ReadAsStringAsync();
            var Country = await v.Contents[4].ReadAsStringAsync();
            var Email = await v.Contents[5].ReadAsStringAsync();
            var Password = await v.Contents[6].ReadAsStringAsync();
            var PhoneNum = await v.Contents[7].ReadAsStringAsync();
            var Image = await v.Contents[8].ReadAsStreamAsync();

            byte[] imageBytes = await ConvertStreamToByteArrayAsync(Image);

            try
            {
                if (!await Repo.CheckIfAlreadyExists(Email))
                {
                    User u = new User(FirstName,
                        LastName,
                        Password,
                        Address,
                        City,
                        Country,
                        PhoneNum,
                        Email);
                    Guid guid = Guid.NewGuid();
                    u.RowKey = guid.ToString();
                    u.UserId = guid;
                    string imageUrl = await blob.UploadImage(imageBytes, u.UserId);
                    u.Image = imageUrl;
                    Repo.AddUser(u);

                    return Ok("Successfully registered new user!");
                }
                else
                {
                    return BadRequest("User already exists!");
                }
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        public async Task<byte[]> ConvertStreamToByteArrayAsync(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }



}
