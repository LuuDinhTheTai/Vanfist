using System.ComponentModel.DataAnnotations;

namespace Vanfist.Entities;

public class Attachment
{
    public int Id { get; set; }

    [Required, StringLength(255)]
    public string FileName { get; set; } = default!;   // tên file lưu (unique/safe)

    [Required, StringLength(255)]
    public string Type { get; set; } = default!;       // "image" / "pdf"

    [StringLength(255)]
    public string? OriginalName { get; set; }

    [StringLength(100)]
    public string? ContentType { get; set; }

    public long Size { get; set; }

    [Required, StringLength(500)]
    public string FilePath { get; set; } = default!;   // ví dụ "/uploads/2025/09/xxx.jpg"

    [StringLength(255)]
    public string? Title { get; set; }

    [StringLength(255)]
    public string? AltText { get; set; }

    public int? SortOrder { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Gắn với Model (đang có sẵn)
    public int ModelId { get; set; }
    public Model Model { get; set; } = default!;
}
