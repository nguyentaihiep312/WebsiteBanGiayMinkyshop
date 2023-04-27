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
    public class DanhGiaConfiguration : IEntityTypeConfiguration<DanhGia>
    {
        public void Configure(EntityTypeBuilder<DanhGia> builder)
        {
            builder.ToTable("DanhGia");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.SoDanhGia).HasDefaultValue(5);

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.KhachHang).WithMany(c => c.DanhGias).HasForeignKey(c => c.IdKhachHang).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.HoaDon).WithOne(c => c.DanhGia).HasForeignKey<DanhGia>(c => c.IdHoaDon);
        }
    }
}
