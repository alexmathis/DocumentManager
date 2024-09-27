using Microsoft.AspNetCore.Http;

namespace DocumentManager.Domain.Interfaces
{
    public interface IFileStorageService
    {
        /// <summary>
        /// Saves the file using IFormFile and returns the saved file's path.
        /// </summary>
        Task<string> SaveFileAsync(IFormFile file, string directoryPath);

        /// <summary>
        /// Retrieves the file's byte[] content from the specified path.
        /// </summary>
        Task<byte[]> GetFileAsync(string filePath);

        /// <summary>
        /// Deletes the file at the specified path.
        /// </summary>
        Task<bool> DeleteFileAsync(string filePath);
    }
}
