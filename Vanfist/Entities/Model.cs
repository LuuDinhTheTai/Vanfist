using System.ComponentModel.DataAnnotations;

namespace Vanfist.Entities
{
    public class Model
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [StringLength(255, ErrorMessage = "Tên sản phẩm không được vượt quá 255 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public float? Price { get; set; }

        [Required(ErrorMessage = "Chiều dài là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều dài phải lớn hơn 0")]
        public float? Length { get; set; } // mm

        [Required(ErrorMessage = "Chiều rộng là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều rộng phải lớn hơn 0")]
        public float? Width { get; set; } // mm

        [Required(ErrorMessage = "Chiều cao là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều cao phải lớn hơn 0")]
        public float? Height { get; set; } // mm

        [Required(ErrorMessage = "Chiều dài cơ sở là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều dài cơ sở phải lớn hơn 0")]
        public float? Wheelbase { get; set; } // mm

        [Required(ErrorMessage = "NEDC là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "NEDC phải lớn hơn 0")]
        public float? NEDC { get; set; } // km/lần sạc

        [Required(ErrorMessage = "Công suất tối đa là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Công suất tối đa phải lớn hơn 0")]
        public float? MaximumPower { get; set; } // kW/hp

        [Required(ErrorMessage = "Mô men xoắn tối đa là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Mô men xoắn tối đa phải lớn hơn 0")]
        public float? MaximumTorque { get; set; } // Nm

        [Required(ErrorMessage = "Kích thước la-zăng là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Kích thước la-zăng phải lớn hơn 0")]
        public float? RimSize { get; set; } // inch

        [Required(ErrorMessage = "Màu sắc là bắt buộc")]
        public string? Color { get; set; }


        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
