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
    public class BienTheChiTietConfiguration : IEntityTypeConfiguration<BienTheChiTiet>
    {
        public void Configure(EntityTypeBuilder<BienTheChiTiet> builder)
        {
            builder.ToTable("BienTheChiTiet");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.ThuocTinhSanPham).WithMany(c => c.BienTheChiTiets).HasForeignKey(c => c.IdThuocTinhSanPham).OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(c => c.BienThe).WithMany(c => c.BienTheChiTiets).HasForeignKey(c => c.IdBienThe).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.GiaTri).WithMany(c => c.BienTheChiTiets).HasForeignKey(c => c.IdGiaTri).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
