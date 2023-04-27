using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Configurations
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("HoaDon");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.LoaiDonHang).HasDefaultValue(0);

            builder.Property(c => c.TienShip).HasDefaultValue(0);

            builder.Property(c => c.TongTien).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.KhachHang).WithMany(c => c.HoaDons).HasForeignKey(c => c.IdKhachHang).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.NhanVien).WithMany(c => c.HoaDons).HasForeignKey(c => c.IdNhanVien).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
