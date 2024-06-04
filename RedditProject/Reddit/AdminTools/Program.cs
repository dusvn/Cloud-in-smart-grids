using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdminTools
{
    public class Program
    {
        static void Main(string[] args)
        {

            IHealthMonitoring proxy;

            var binding = new NetTcpBinding();
            ChannelFactory<IHealthMonitoring> factory = new
            ChannelFactory<IHealthMonitoring>(binding, new
            EndpointAddress("net.tcp://localhost:10178/hms-admin"));

            proxy = factory.CreateChannel();

            Admins admins = new Admins();
            Console.WriteLine("************Admin Tools*************\n");

            while (true)
            {
                Console.WriteLine("Enter username:");
                string username=Console.ReadLine();

                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();

                if (username.ToUpper().Equals("X") && password.ToUpper().Equals("X")) { break; }

                if (!admins.Users.ContainsKey(username)) { Console.WriteLine("Wrong credentials!");continue; }

                if (!(admins.Users[username].Equals(password))) { Console.WriteLine("Wrong credentials!"); continue; }


                while (true)
                {


                    Console.WriteLine("Please choose option from menu:");
                    Console.WriteLine("-1.Add email");
                    Console.WriteLine("-2.Log out");

                    string s = Console.ReadLine();


                    if (!s.Equals("1") && !s.Equals("2")) { Console.WriteLine("Please choose right menu option !"); continue; }

                    if (s.Equals("2")) { break; }


                    Console.WriteLine("Type email:");

                    string email=Console.ReadLine();

                    var pattern = @"^(?:(?:[a-zA-Z0-9][a-zA-Z0-9._%+-]*@[a-zA-Z0-9.-]+\.[A-Za-z]{2,})|(?:[\w\.-]+@[\w\.-]+\.\w+))$";

                    var regex = new Regex(pattern);

                    if (!regex.IsMatch(email)) { Console.WriteLine("You must enter vaild email !"); continue; }

                    var ifExists = proxy.AddHealthEmail(email);

                    if (ifExists) { Console.WriteLine($"Email already exists try another one!");continue; }

                    Console.WriteLine("Email added successfully");


                }



            }

            Console.WriteLine("Exiting admin tools....");
            
            

        }
    }
}
