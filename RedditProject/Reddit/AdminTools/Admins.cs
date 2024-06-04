using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminTools
{
    public class Admins
    {

        public Dictionary<string, string> Users { get; }


        public Admins() 
        { 
            
            Users = new Dictionary<string, string>
            {
                { "admin", "admin" }
            };
        
        }


    }
}
