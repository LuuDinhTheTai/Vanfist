namespace Vanfist.DTOs.Responses;

public class TestDriveRequestItem
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ModelName { get; set; }
    public DateTime? PreferredTime { get; set; }
    public string Status { get; set; } = "New";
    public DateTime CreatedAt { get; set; }
}
