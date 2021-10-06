

using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace vetappback.Utilities
{
    public class AzureStorage : IFileStorage
    {
        private string connectionString;
        public AzureStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }


        public async Task<string> SaveFile(string container, IFormFile file)
        {

            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
            var xtension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{xtension}";
            var blob = client.GetBlobClient(fileName);
            await blob.UploadAsync(file.OpenReadStream());
            return blob.Uri.ToString();

        }

        public async Task DeleteFile(string route, string container)
        {

            if (string.IsNullOrEmpty(route))
            {
                return;
            }

            var client = new BlobContainerClient(connectionString, container);
            await client.CreateIfNotExistsAsync();
            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }


        public async Task<string> EditFile(string container, IFormFile file, string route)
        {
            await DeleteFile(route, container);

            return await SaveFile(container, file);
        }
    }
}