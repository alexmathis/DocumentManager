
//using Azure.Storage.Blobs;
//using Azure.Storage.Blobs.Models;
//using DocumentManager.Domain.Interfaces;
//using System;
//using System.IO;
//using System.Threading.Tasks;

//namespace DocumentManager.Infrastructure.Services;

//public class FileStorageService : IFileStorageService
//{
//    private readonly BlobServiceClient _blobServiceClient;

//    public FileStorageService(string connectionString)
//    {
//        _blobServiceClient = new BlobServiceClient(connectionString);
//    }

//    /// <summary>
//    /// Saves a file to Azure Blob Storage.
//    /// </summary>
//    public async Task<string> SaveFileAsync(string fileName, byte[] fileContent, string containerName)
//    {
//        // Get a reference to a container and create it if it doesn't exist
//        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
//        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

//        // Get a reference to the blob
//        var blobClient = containerClient.GetBlobClient(fileName);

//        // Upload the file
//        using (var stream = new MemoryStream(fileContent))
//        {
//            await blobClient.UploadAsync(stream, overwrite: true);
//        }

//        // Return the URL to the blob
//        return blobClient.Uri.ToString();
//    }

//    /// <summary>
//    /// Retrieves a file from Azure Blob Storage.
//    /// </summary>
//    public async Task<byte[]> GetFileAsync(string blobUrl)
//    {
//        // Create a blob client
//        var blobClient = new BlobClient(new Uri(blobUrl));

//        // Check if the blob exists
//        if (!await blobClient.ExistsAsync())
//        {
//            throw new FileNotFoundException($"The file at {blobUrl} was not found.");
//        }

//        // Download the blob's contents as a memory stream
//        BlobDownloadInfo download = await blobClient.DownloadAsync();

//        using (var memoryStream = new MemoryStream())
//        {
//            await download.Content.CopyToAsync(memoryStream);
//            return memoryStream.ToArray();
//        }
//    }

//    /// <summary>
//    /// Deletes a file from Azure Blob Storage.
//    /// </summary>
//    public async Task<bool> DeleteFileAsync(string blobUrl)
//    {
//        // Create a blob client
//        var blobClient = new BlobClient(new Uri(blobUrl));

//        // Delete the blob if it exists
//        var result = await blobClient.DeleteIfExistsAsync();

//        // Return whether the deletion was successful
//        return result;
//    }
//}
