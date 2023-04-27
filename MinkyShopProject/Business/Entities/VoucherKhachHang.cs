using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class VoucherKhachHang
    {
        public Guid Id { get; set; }

        public Guid IdVoucher { get; set; }

        public Guid IdKhachHang { get; set; }

        public int TrangThai { get; set; }

        public Voucher Voucher { get; set; } = null!;

        public KhachHang KhachHang { get; set; } = null!;
    }
}
