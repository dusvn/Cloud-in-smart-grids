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

namespace RedditService.Controllers
{
    public class LogInController : ApiController
    {

        UserDataRepository repo = new UserDataRepository();
        BlobHelper blob = new BlobHelper();

        public UserDataRepository Repo { get => repo; set => repo = value; }

        [HttpPost]
        [ActionName("PostLogin")]
        [Route("login")]
        public async Task<IHttpActionResult> PostLogin() 
        {
            return Ok();
            //if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) return BadRequest("Password and email can't be empty!");
            //try
            //{
            //    User userInfo = await Repo.Login(user.Email, user.Password);
            //    // take image of user from blob 
            //    byte[] userImage = await blob.DownloadImage(userInfo, "users"); // don't touch small letter is required!

            //    if (userInfo == null) return BadRequest("Invalid credentials!");
            //    else
            //    {
            //        var response = new
            //        {
            //            user = new FullUserDTO(userInfo.FirstName,
            //            userInfo.LastName,
            //            userInfo.Password,
            //            userInfo.Address,
            //            userInfo.City,
            //            userInfo.Country,
            //            userInfo.PhoneNum,
            //            userInfo.Email,
            //            userImage, // image from blob 
            //            userInfo.UserId),
            //            message = "Login successful"
            //        };
            //        return Ok(response);
            //    }
            //}
            //catch (Exception)
            //{
            //    return StatusCode(HttpStatusCode.InternalServerError);
            //}
        }


        [HttpPost]
        [Route("login/register")]
        public async Task<IHttpActionResult> Register([FromBody]UserRegister userData)
        {

            if (string.IsNullOrEmpty(userData.Email)) return BadRequest("Invalid email format");
            if (string.IsNullOrEmpty(userData.Password)) return BadRequest("Password cannot be null or empty");
            if (string.IsNullOrEmpty(userData.FirstName)) return BadRequest("First name cannot be null or empty");
            if (string.IsNullOrEmpty(userData.LastName)) return BadRequest("Last name cannot be null or empty");
            if (string.IsNullOrEmpty(userData.Address)) return BadRequest("Address cannot be null or empty");
            if (string.IsNullOrEmpty(userData.PhoneNum)) return BadRequest("Phone num cannot be null or empty");
            if (string.IsNullOrEmpty(userData.Country)) return BadRequest("Country cannot be null or empty");
            if (string.IsNullOrEmpty(userData.City)) return BadRequest("City cannot be null or empty");
            if (userData.Image.Length == 0 || userData.Image == null) return BadRequest("Image is required!");
            try
            {
                if (!await Repo.CheckIfAlreadyExists(userData.Email))
                {
                    User u = new User(userData.FirstName,
                        userData.LastName,
                        userData.Password,
                        userData.Address,
                        userData.City,
                        userData.Country,
                        userData.PhoneNum,
                        userData.Email);

                    //photo upload
                    string imageUrl = await blob.UploadImage(userData.Image, userData.UserId);
                    u.Image = imageUrl;
                    Guid guid = Guid.NewGuid();
                    u.RowKey = guid.ToString();
                    u.UserId = guid;

                    //onda se dodaje user 
                    Repo.AddUser(u);

                    return Ok("Successfuly register new user!");
                
                } else return BadRequest("User already exists!");
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


    }


}
