using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditService.Provider
{
    public class RedditProvider : IRedditService
    {
        public string Message()
        {
            return "Hi from RedditService";
        }
    }
}