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
    public class HoaDonChiTietConfiguration : IEntityTypeConfiguration<HoaDonChiTiet>
    {
        public void Configure(EntityTypeBuilder<HoaDonChiTiet> builder)
        {
            builder.ToTable("HoaDonChiTiet");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.SoLuong).HasDefaultValue(0);

            builder.Property(c => c.DonGia).HasDefaultValue(0);

            builder.HasOne(c => c.BienThe).WithMany(c => c.HoaDonChiTiets).HasForeignKey(c => c.IdBienThe).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.HoaDon).WithMany(c => c.HoaDonChiTiets).HasForeignKey(c => c.IdHoaDon).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
