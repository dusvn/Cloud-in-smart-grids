using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Reddit_Data;
using Common.Models;
using Common.DTO;
using System.Collections.Generic;
using System.Web;

namespace RedditService.Controllers
{
    public class UserController : ApiController
    {
        private UserDataRepository repo = new UserDataRepository();
        private BlobHelper blob = new BlobHelper();

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

            var provider = await Request.Content.ReadAsMultipartAsync();
            var firstName = await provider.Contents[0].ReadAsStringAsync();
            var lastName = await provider.Contents[1].ReadAsStringAsync();
            var city = await provider.Contents[2].ReadAsStringAsync();
            var address = await provider.Contents[3].ReadAsStringAsync();
            var country = await provider.Contents[4].ReadAsStringAsync();
            var email = await provider.Contents[5].ReadAsStringAsync();
            var password = await provider.Contents[6].ReadAsStringAsync();
            var phoneNum = await provider.Contents[7].ReadAsStringAsync();
            var image = await provider.Contents[8].ReadAsStreamAsync();

            byte[] imageBytes = await ConvertStreamToByteArrayAsync(image);

            try
            {
                if (!await repo.CheckIfAlreadyExists(email))
                {
                    var user = new User(firstName, lastName, password, address, city, country, phoneNum, email)
                    {
                        RowKey = Guid.NewGuid().ToString(),
                        UserId = Guid.NewGuid()
                    };

                    string imageUrl = await blob.UploadImage(imageBytes, user.UserId);
                    user.Image = imageUrl;
                    repo.AddUser(user);

                    return Ok("Successfully registered new user!");
                }
                else
                {
                    return BadRequest("User already exists!");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        [Route("user/changeFields")]
        public async Task<IHttpActionResult> UpdateUser()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            var firstName = await provider.Contents[0].ReadAsStringAsync();
            var lastName = await provider.Contents[1].ReadAsStringAsync();
            var address = await provider.Contents[2].ReadAsStringAsync();
            var city = await provider.Contents[3].ReadAsStringAsync();
            var country = await provider.Contents[4].ReadAsStringAsync();
            var email = await provider.Contents[5].ReadAsStringAsync();
            var phoneNumber = await provider.Contents[6].ReadAsStringAsync();
            var image = await provider.Contents[7].ReadAsStreamAsync();
            var newPassword = await provider.Contents[8].ReadAsStringAsync();
            var id = await provider.Contents[9].ReadAsStringAsync();

            var user = await repo.GetUserInfo(Guid.Parse(id));
            if (user == null)
            {
                return NotFound();
            }

            bool isUpdated = false;

            if (!string.Equals(firstName, user.FirstName, StringComparison.Ordinal))
            {
                user.FirstName = firstName;
                isUpdated = true;
            }
            if (!string.Equals(country, user.Country, StringComparison.Ordinal))
            {
                user.Country = country;
                isUpdated = true;
            }
            if (!string.Equals(lastName, user.LastName, StringComparison.Ordinal))
            {
                user.LastName = lastName;
                isUpdated = true;
            }
            if (!string.Equals(address, user.Address, StringComparison.Ordinal))
            {
                user.Address = address;
                isUpdated = true;
            }
            if (!string.Equals(city, user.City, StringComparison.Ordinal))
            {
                user.City = city;
                isUpdated = true;
            }
            if (!string.Equals(email, user.Email, StringComparison.Ordinal))
            {
                user.Email = email;
                isUpdated = true;
            }
            if (!string.Equals(phoneNumber, user.PhoneNum, StringComparison.Ordinal))
            {
                user.PhoneNum = phoneNumber;
                isUpdated = true;
            }
            if (!string.Equals(newPassword, user.Password, StringComparison.Ordinal) && newPassword!="")
            {
                user.Password = newPassword;
                isUpdated = true;
            }
            
            

            if (image != null && image.Length > 4)
            {
                byte[] imageBytes = await ConvertStreamToByteArrayAsync(image);
                string imageUrl = await blob.UploadImage(imageBytes, Guid.Parse(id));
                user.Image = imageUrl;
                isUpdated = true;
            }

            if (isUpdated)
            {
                await repo.UpdateUser(user);
            }

            byte[] imageResponse = await blob.DownloadImage(user.UserId.ToString(), "users");
            var response = new
            {
                changedUser = new FullUserDTO(user.FirstName, user.LastName, user.Password, user.Address, user.City, user.Country, user.PhoneNum, user.Email, imageResponse, user.UserId)
            };

            return Ok(response);
        }

        private async Task<byte[]> ConvertStreamToByteArrayAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }


        [HttpGet]
        [Route("user/getProfileInfo")]
        public async Task<IHttpActionResult> GetProfileInfo(Guid id)
        {
            var user = await repo.GetUserInfo(id);
            if (user == null) return BadRequest("User does not exist!");

            byte[] image = await blob.DownloadImage(id.ToString(), "users");

            var response = new
            {
                message = "Successfully retrieved user info",
                user = new FullUserDTO(user.FirstName, user.LastName, user.Password, user.Address, user.City, user.Country, user.PhoneNum, user.Email, image, user.UserId)
            };

            return Ok(response);
        }

    }
}
