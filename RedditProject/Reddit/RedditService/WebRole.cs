using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using RedditService.Provider;

namespace RedditService
{
    public class WebRole : RoleEntryPoint
    {

        private RedditServer redditServer;

        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at https://go.microsoft.com/fwlink/?LinkId=166357.

            bool result=base.OnStart();
            redditServer= new RedditServer();
            redditServer.Open();


            return result;
        }


        public override void OnStop() 
        { 
            
            base.OnStop();
            redditServer.Close();
        
        }   

    }
}
