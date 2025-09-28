namespace Vanfist.Services;

public interface IFileStorage
{
    Task<(string savedFileName, string relativePath)> SaveAsync(Stream stream, string originalName, string contentType);
    Task DeleteAsync(string relativePath);
}
