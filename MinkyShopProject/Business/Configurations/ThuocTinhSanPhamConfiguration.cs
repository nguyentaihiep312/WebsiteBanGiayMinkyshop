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
    public class ThuocTinhSanPhamConfiguration : IEntityTypeConfiguration<ThuocTinhSanPham>
    {
        public void Configure(EntityTypeBuilder<ThuocTinhSanPham> builder)
        {
            builder.ToTable("ThuocTinhSanPham");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasOne(c => c.SanPham).WithMany(c => c.ThuocTinhSanPhams).HasForeignKey(c => c.IdSanPham).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.ThuocTinh).WithMany(c => c.ThuocTinhSanPhams).HasForeignKey(c => c.IdThuocTinh).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
