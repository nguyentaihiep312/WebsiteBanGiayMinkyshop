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
    public class HinhThucThanhToanConfiguration : IEntityTypeConfiguration<HinhThucThanhToan>
    {
        public void Configure(EntityTypeBuilder<HinhThucThanhToan> builder)
        {
            builder.ToTable("HinhThucThanhToan");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.KieuThanhToan).HasDefaultValue(0);

            builder.Property(c => c.TongTienThanhToan).HasDefaultValue(0);

            builder.HasOne(c => c.HoaDon).WithMany(c => c.HinhThucThanhToans).HasForeignKey(c => c.IdHoaDon).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
