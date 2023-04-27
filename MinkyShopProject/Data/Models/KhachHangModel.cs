using MinkyShopProject.Common;
using System.ComponentModel.DataAnnotations;

namespace MinkyShopProject.Data.Models
{
    public class KhachHangModel
    {
        public Guid Id { get; set; }

        public Guid? IdViDiem { get; set; }

        public string? Ma { get; set; } = null!;

        public string? Ten { get; set; } = null!;

        public string? Anh { get; set; } = null!;

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public DateTime NgayTao { get; set; }

        public string? DiaChi { get; set; } = null!;

        public string? Sdt { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? MatKhau { get; set; } = null!;

        public int SoLanMua { get; set; }
    }
    public class KhachHangThemVaSuaModel
    {

        public Guid Id { get; set; }

        public Guid? IdViDiem { get; set; }

        public string? Ma { get; set; } = null!;

        [Required(ErrorMessage = "Tên không được bỏ trống")]
        public string? Ten { get; set; } = null!;

        public string? Anh { get; set; } = null!;

        public bool GioiTinh { get; set; } = true;

        [CheckAge(ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime NgaySinh { get; set; } = DateTime.Now;

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public string? DiaChi { get; set; } = null!;

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Số điện thoại phải là số")]
        public string? Sdt { get; set; } = null!;

        [RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; } = null!;

        public string? MatKhau { get; set; } = null!;

        public string? MatKhauNhapLai { get; set; } = null!;

        public int SoLanMua { get; set; }
    }
    public class KhachHangQueryModel : PaginationRequest
    {
        public Guid? khachhangId { get; set; }
        public string? Ten { get; set; }
    }
}
