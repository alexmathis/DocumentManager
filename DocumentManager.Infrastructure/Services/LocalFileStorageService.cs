using Microsoft.AspNetCore.Http;
using DocumentManager.Domain.Interfaces;

public class LocalFileStorageService : IFileStorageService
{
    public async Task<string> SaveFileAsync(IFormFile file, string directoryPath = "uploads/documents")
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var fileName = Path.GetFileName(file.FileName);
        var fullPath = Path.Combine(directoryPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(stream);  
        }

        return fullPath; 
    }

    public Task<byte[]> GetFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }

        return File.ReadAllBytesAsync(filePath);
    }

    public Task<bool> DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);  
    }
}
