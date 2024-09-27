using Microsoft.AspNetCore.Http;
using DocumentManager.Domain.Interfaces;
using System.Threading.Tasks;

public class MockAzureFileStorageService : IFileStorageService
{
    // This method simulates saving a file, but doesn't actually save anything
    public Task<string> SaveFileAsync(IFormFile file, string directoryPath = "uploads/documents")
    {
        // Return a dummy file path to simulate successful file save
        return Task.FromResult("mock/file/path/" + file.FileName);
    }

    // This method simulates retrieving a file, but doesn't actually retrieve anything
    public Task<byte[]> GetFileAsync(string filePath)
    {
        // Return an empty byte array to simulate file retrieval
        return Task.FromResult(new byte[0]);
    }

    // This method simulates deleting a file, but doesn't actually delete anything
    public Task<bool> DeleteFileAsync(string filePath)
    {
        // Return true to simulate successful deletion
        return Task.FromResult(true);
    }
}
