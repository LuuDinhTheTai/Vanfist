using System.ComponentModel.DataAnnotations;

namespace Vanfist.DTOs.Requests
{
    public class ModelRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(255, ErrorMessage = "Tên sản phẩm tối đa 255 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Chiều dài không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều dài phải là số dương")]
        public float Length { get; set; }

        [Required(ErrorMessage = "Chiều rộng không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều rộng phải là số dương")]
        public float Width { get; set; }

        [Required(ErrorMessage = "Chiều cao không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều cao phải là số dương")]
        public float Height { get; set; }

        [Required(ErrorMessage = "Chiều dài cơ sở không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Chiều dài cơ sở phải là số dương")]
        public float Wheelbase { get; set; }

        [Required(ErrorMessage = "Quãng đường (NEDC) không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "NEDC phải là số dương")]
        public float NEDC { get; set; }

        [Required(ErrorMessage = "Công suất tối đa không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Công suất tối đa phải là số dương")]
        public float MaximumPower { get; set; }

        [Required(ErrorMessage = "Mô men xoắn tối đa không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Mô men xoắn phải là số dương")]
        public float MaximumTorque { get; set; }

        [Required(ErrorMessage = "Kích thước vành không được để trống")]
        [Range(1, double.MaxValue, ErrorMessage = "Kích thước vành phải là số dương")]
        public float RimSize { get; set; }

        [Required(ErrorMessage = "Màu sắc không được để trống")]
        [RegularExpression(@"^[a-zA-Z\sÀ-ỹ]+$", ErrorMessage = "Màu sắc chỉ được nhập chữ")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phải chọn danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Danh mục không hợp lệ")]
        public int CategoryId { get; set; }
    }
}
