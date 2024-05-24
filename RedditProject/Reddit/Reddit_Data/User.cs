using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddit_Data
{
    public class User:TableEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }

        public string City { get; set; }    

        public string Country { get; set; }
        public string PhoneNum { get; set; }    

        public string Email { get;set; }
        
        public string Image { get; set; }   

        public Guid UserId { get; set; }

        public User(string firstName, string lastName, string password, string address, string city, string country, string phoneNum, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Address = address;
            City = city;
            Country = country;
            PhoneNum = phoneNum;
            Email = email;
            PartitionKey = "User";
        }

        public User() { }


    }
}
