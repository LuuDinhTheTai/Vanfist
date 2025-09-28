using Microsoft.Extensions.Options;

namespace Vanfist.Services.Impl;

public class LocalFileStorage : IFileStorage
{
    private readonly string _root;
    private readonly string _baseUrl;
    private readonly long _maxSizeBytes;

    public class UploadsOptions
    {
        public string Root { get; set; } = "wwwroot/uploads";
        public string BaseUrl { get; set; } = "/uploads";
        public int MaxSizeMB { get; set; } = 10;
    }

    public LocalFileStorage(IOptions<UploadsOptions> opt)
    {
        _root = opt.Value.Root;
        _baseUrl = opt.Value.BaseUrl;
        _maxSizeBytes = (long)opt.Value.MaxSizeMB * 1024 * 1024;
        Directory.CreateDirectory(_root);
    }

    public async Task<(string savedFileName, string relativePath)> SaveAsync(Stream stream, string originalName, string contentType)
    {
        // validate size (nếu stream có Length)
        if (stream.CanSeek && stream.Length > _maxSizeBytes)
            throw new InvalidOperationException("File quá lớn");

        var ext = Path.GetExtension(originalName);
        var safeName = $"{Guid.NewGuid():N}{ext}".ToLowerInvariant();

        var today = DateTime.UtcNow;
        var folder = Path.Combine(_root, today.Year.ToString(), today.Month.ToString("D2"));
        Directory.CreateDirectory(folder);

        var fullPath = Path.Combine(folder, safeName);
        using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
        {
            await stream.CopyToAsync(fs);
        }

        // relative path dùng để build URL
        var relative = $"{_baseUrl}/{today.Year}/{today.Month:D2}/{safeName}".Replace("\\", "/");
        return (safeName, relative);
    }

    public Task DeleteAsync(string relativePath)
    {
        var trimmed = relativePath.TrimStart('~').TrimStart('/');
        var full = Path.Combine(_root, "..", trimmed.Replace("/", Path.DirectorySeparatorChar.ToString()));
        // build lại full path an toàn
        var rooted = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, full));
        if (File.Exists(rooted)) File.Delete(rooted);
        return Task.CompletedTask;
    }
}
