using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Reddit_Data
{
    public class BlobHelper
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobStorage;

        public BlobHelper()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));
            blobStorage = storageAccount.CreateCloudBlobClient();
        }

        public CloudBlockBlob GetBlockBlobReference(string containerName, string blobName)
        {
            CloudBlobContainer container = blobStorage.GetContainerReference(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);

            return blob;
        }

        public async Task<byte[]> DownloadImage(string id, string nameOfContainer)
        {

            CloudBlockBlob blob = GetBlockBlobReference(nameOfContainer, $"image_{id}");
            await blob.FetchAttributesAsync();
            long blobLength = blob.Properties.Length;
            byte[] byteArray = new byte[blobLength];
            await blob.DownloadToByteArrayAsync(byteArray, 0);

            return byteArray;

        }

        public  async Task<bool> DeleteFileFromComments(Guid commentId)
        {
            try
            {
                
                CloudBlockBlob blob = GetBlockBlobReference("comments", $"text_{commentId}");
                await blob.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> DeleteFilesFromPosts(Guid postId)
        {
            try
            {

                CloudBlockBlob blob1 = GetBlockBlobReference("posts", $"text_{postId}");
                CloudBlockBlob blob2 = GetBlockBlobReference("posts", $"image_{postId}");

                await blob1.DeleteAsync();
                await blob2.DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }




        public async Task<string> UploadImage(byte[] image,Guid userId)
        {
            CloudBlockBlob blob = GetBlockBlobReference("users", $"image_{userId}");
            blob.Properties.ContentType = "image";
            await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            return blob.Uri.AbsoluteUri;
        }

        public async Task<string> UploadImagePost(byte[] image, Guid userId)
        {
            CloudBlockBlob blob = GetBlockBlobReference("posts", $"image_{userId}");
            blob.Properties.ContentType = "image";
            await blob.UploadFromByteArrayAsync(image, 0, image.Length);
            return blob.Uri.AbsoluteUri;
        }



        public async Task<string> UploadText(byte[] text, Guid userId)
        {
            CloudBlockBlob blob = GetBlockBlobReference("posts", $"text_{userId}");
            blob.Properties.ContentType = "text";
            await blob.UploadFromByteArrayAsync(text, 0, text.Length);
            return blob.Uri.AbsoluteUri;
        }

        public async Task<string> UploadComment(byte[] text, Guid commentId)
        {
            CloudBlockBlob blob = GetBlockBlobReference("comments", $"text_{commentId}");
            blob.Properties.ContentType = "text";
            await blob.UploadFromByteArrayAsync(text, 0, text.Length);
            return blob.Uri.AbsoluteUri;
        }


        public async Task<byte[]> DownloadComment(string id, string nameOfContainer)
        {

            CloudBlockBlob blob = GetBlockBlobReference(nameOfContainer, $"text_{id}");
            await blob.FetchAttributesAsync();
            long blobLength = blob.Properties.Length;
            byte[] byteArray = new byte[blobLength];
            await blob.DownloadToByteArrayAsync(byteArray, 0);

            return byteArray;

        }


        public async Task<byte[]> DownloadText(string id, string nameOfContainer)
        {

            CloudBlockBlob blob = GetBlockBlobReference(nameOfContainer, $"text_{id}");
            await blob.FetchAttributesAsync();
            long blobLength = blob.Properties.Length;
            byte[] byteArray = new byte[blobLength];
            await blob.DownloadToByteArrayAsync(byteArray, 0);

            return byteArray;

        }



    }
}
