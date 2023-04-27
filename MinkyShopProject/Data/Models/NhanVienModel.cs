using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public static class NhanVienModel
    {
        public class NhanVienCreateModel
        {
            public string Ma { get; set; } = null!;

            public string Ten { get; set; } = null!;

            public string Anh { get; set; } = null!;

            public bool GioiTinh { get; set; }

            public DateTime? NgaySinh { get; set; }

            public string DiaChi { get; set; } = null!;

            public string Sdt { get; set; } = null!;

            public string Email { get; set; } = null!;
            public string MatKhau { get; set; } = null!;

            public int VaiTro { get; set; }

            public int TrangThai { get; set; }
        }

        public class NhanVienFormModel
        {
           
            public string Ma { get; set; } = null!;

         
            public string Ten { get; set; } = null!;

            public string? Anh { get; set; } = null!;

            public bool GioiTinh { get; set; }

            public DateTime? NgaySinh { get; set; }

            public string DiaChi { get; set; } = null!;

            public string Sdt { get; set; } = null!;

            public string Email { get; set; } = null!;

            public string MatKhau { get; set; } = null!;

            public int VaiTro { get; set; }

            public int TrangThai { get; set; }
        }
    }
}
