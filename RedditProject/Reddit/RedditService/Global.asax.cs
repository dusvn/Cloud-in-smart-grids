using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RedditService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            InitBlobsUsers();
            InitBlobsPosts();
            InitBlobsComments();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }


        public void InitBlobsUsers()
        {
            try
            {
                // read account configuration settings
                var storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

                // create blob container for images
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference("users");
                container.CreateIfNotExists();
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            catch (WebException)
            {
            }
        }


        public void InitBlobsComments()
        {
            try
            {
                // read account configuration settings
                var storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

                // create blob container for images
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference("comments");
                container.CreateIfNotExists();
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            catch (WebException)
            {
            }
        }

        public void InitBlobsPosts()
        {
            try
            {
                // read account configuration settings
                var storageAccount =
                CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

                // create blob container for images
                CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobStorage.GetContainerReference("posts");
                container.CreateIfNotExists();
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }
            catch (WebException)
            {
            }
        }





    }
}
