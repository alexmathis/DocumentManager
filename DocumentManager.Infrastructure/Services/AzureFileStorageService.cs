using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocumentManager.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public FileStorageService(IConfiguration configuration)
        {
            // Use the indexer to get the connection string from configuration
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        /// <summary>
        /// Saves a file to Azure Blob Storage.
        /// </summary>
        public async Task<string> SaveFileAsync(string fileName, byte[] fileContent, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(fileContent))
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString();
        }

        public async Task<string> SaveFileAsync(IFormFile file, string containerName)
        {
            var fileName = Path.GetFileName(file.FileName);

            // Read the file content into a byte array
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream); 
                var fileContent = memoryStream.ToArray(); 

                return await SaveFileAsync(fileName, fileContent, containerName);
            }
        }


        /// <summary>
        /// Retrieves a file from Azure Blob Storage.
        /// </summary>
        public async Task<byte[]> GetFileAsync(string blobUrl)
        {

            var blobClient = new BlobClient(new Uri(blobUrl));

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException($"The file at {blobUrl} was not found.");
            }


            BlobDownloadInfo download = await blobClient.DownloadAsync();

            using (var memoryStream = new MemoryStream())
            {
                await download.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Deletes a file from Azure Blob Storage.
        /// </summary>
        public async Task<bool> DeleteFileAsync(string blobUrl)
        {
            var blobClient = new BlobClient(new Uri(blobUrl));

            var result = await blobClient.DeleteIfExistsAsync();

            return result.Value;
        }
    }
}
