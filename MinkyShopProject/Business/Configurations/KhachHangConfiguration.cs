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
	public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
	{
		public void Configure(EntityTypeBuilder<KhachHang> builder)
		{
			builder.ToTable("KhachHang");

			builder.HasKey(x => x.Id);

			builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

			builder.HasIndex(c => c.Sdt).IsUnique();

			builder.HasIndex(c => c.Email).IsUnique();

			builder.Property(c => c.NgaySinh);

			builder.Property(c => c.SoLanMua).HasDefaultValue(0);

			builder.Property(c => c.TrangThai).HasDefaultValue(0);

			builder.HasOne(c => c.ViDiem).WithOne(c => c.KhachHang).HasForeignKey<KhachHang>(c => c.IdViDiem).OnDelete(DeleteBehavior.Cascade);
		}
	}
}
