using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MinkyShopProject.Business.Configurations
{
    public class ThuocTinhConfiguration : IEntityTypeConfiguration<ThuocTinh>
    {
        public void Configure(EntityTypeBuilder<ThuocTinh> builder)
        {
            builder.ToTable("ThuocTinh");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.TrangThai).HasDefaultValue(0);
        }
    }
}
