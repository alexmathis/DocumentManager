namespace DocumentManager.Domain.Interfaces;

using System.Threading.Tasks;

public interface IFileStorageService
{
    /// <summary>
    /// Saves a file to the storage.
    /// </summary>
    /// <param name="fileName">The name of the file to save.</param>
    /// <param name="fileContent">The byte content of the file.</param>
    /// <param name="directoryPath">The directory where the file will be stored.</param>
    /// <returns>The full path where the file is saved.</returns>
    Task<string> SaveFileAsync(string fileName, byte[] fileContent, string directoryPath);

    /// <summary>
    /// Retrieves a file from the storage.
    /// </summary>
    /// <param name="filePath">The full path of the file to retrieve.</param>
    /// <returns>The byte content of the file.</returns>
    Task<byte[]> GetFileAsync(string filePath);

    /// <summary>
    /// Deletes a file from the storage.
    /// </summary>
    /// <param name="filePath">The full path of the file to delete.</param>
    /// <returns>A boolean indicating whether the file was successfully deleted.</returns>
    Task<bool> DeleteFileAsync(string filePath);
}
