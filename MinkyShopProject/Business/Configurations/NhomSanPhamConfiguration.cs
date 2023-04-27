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
    public class NhomSanPhamConfiguration : IEntityTypeConfiguration<NhomSanPham>
    {
        public void Configure(EntityTypeBuilder<NhomSanPham> builder)
        {
            builder.ToTable("NhomSanPham");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())"); ;

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.NhomSanPhamEntity).WithMany(c => c.NhomSanPhams).HasForeignKey(c => c.IdParent).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
