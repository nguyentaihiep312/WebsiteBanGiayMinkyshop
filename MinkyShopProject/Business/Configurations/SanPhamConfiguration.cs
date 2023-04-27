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
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())"); ;

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.Anh).HasDefaultValue("https://reactnative-examples.com/wp-content/uploads/2022/02/default-loading-image.png");

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.NhomSanPham).WithMany(c => c.SanPhams).HasForeignKey(c => c.IdNhomSanPham).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
