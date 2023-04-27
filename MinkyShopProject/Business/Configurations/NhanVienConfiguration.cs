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
    public class NhanVienConfiguration : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> builder)
        {
            builder.ToTable("NhanVien");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.VaiTro).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(0);
        }
    }
}
