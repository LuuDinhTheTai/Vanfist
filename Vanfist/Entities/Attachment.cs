using System.ComponentModel.DataAnnotations;

namespace Vanfist.Entities;

public class Attachment
{
    public int Id { get; set; }

    [StringLength(255)] public string FileName { get; set; }

    [Required, StringLength(255)] public string Type { get; set; } = "image";
    
    public int ModelId { get; set; }
    public Model Model { get; set; }

}