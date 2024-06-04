using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class FullUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        public string PhoneNum { get; set; }

        public string Email { get; set; }

        public byte[] Image { get; set; }

        public Guid UserId { get; set; }

        public FullUserDTO(string firstName, string lastName, string password, string address, string city, string country, string phoneNum, string email, byte[] image, Guid userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Address = address;
            City = city;
            Country = country;
            PhoneNum = phoneNum;
            Email = email;
            Image = image;
            UserId = userId;
        }
    }
}
