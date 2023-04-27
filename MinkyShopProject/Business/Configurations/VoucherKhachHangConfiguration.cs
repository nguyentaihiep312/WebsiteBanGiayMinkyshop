using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Configurations
{
    public class VoucherKhachHangConfiguration : IEntityTypeConfiguration<VoucherKhachHang>
    {
        public void Configure(EntityTypeBuilder<VoucherKhachHang> builder)
        {
            builder.ToTable("VoucherKhachHang");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.TrangThai).HasDefaultValue(0);
        }
    }
}
