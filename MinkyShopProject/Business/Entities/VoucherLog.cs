using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class VoucherLog
    {
        public Guid Id { get; set; }

        public Guid IdHoaDon { get; set; }

        public Guid IdVoucher { get; set; }

        public float TienTruocKhiGiam { get; set; }

        public float TienSauKhiGiam { get; set; }

        public float SoTienGiam { get; set; }

        public DateTime NgayTao { get; set; }

        public HoaDon HoaDon { get; set; } = null!;

        public Voucher Voucher { get; set; } = null!;
    }
}
