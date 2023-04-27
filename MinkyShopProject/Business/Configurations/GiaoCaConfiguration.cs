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
    public class GiaoCaConfiguration : IEntityTypeConfiguration<GiaoCa>
    {
        public void Configure(EntityTypeBuilder<GiaoCa> builder)
        {
            builder.ToTable("GiaoCa");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.TienBanDau);

            builder.Property(c => c.TongTienTrongCa);

            builder.Property(c => c.TongTienMat);

            builder.Property(c => c.TongTienKhac);

            builder.Property(c => c.TienPhatSinh);

            builder.Property(c => c.TongTienMatCaTruoc);

            builder.Property(c => c.TongTienMatRut);

            builder.Property(c => c.TrangThai);

            builder.HasOne(c => c.NhanVien).WithMany(c => c.GiaoCas).HasForeignKey(c => c.IdNhanVienCaTiepTheo).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.NhanVien).WithMany(c => c.GiaoCas).HasForeignKey(c => c.IdNhanVienTrongCa).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
