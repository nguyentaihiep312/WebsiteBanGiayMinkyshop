using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Configurations
{
    public class VoucherLogConfiguration : IEntityTypeConfiguration<VoucherLog>
    {
        public void Configure(EntityTypeBuilder<VoucherLog> builder)
        {
            builder.ToTable("VoucherLog");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.TienTruocKhiGiam).HasDefaultValue(0);

            builder.Property(c => c.TienSauKhiGiam).HasDefaultValue(0);

            builder.Property(c => c.SoTienGiam).HasDefaultValue(0);

            builder.HasOne(c => c.HoaDon).WithMany(c => c.VoucherLogs).HasForeignKey(c => c.IdHoaDon);

            builder.HasOne(c => c.Voucher).WithMany(c => c.VoucherLogs).HasForeignKey(c => c.IdVoucher);
        }
    }
}
