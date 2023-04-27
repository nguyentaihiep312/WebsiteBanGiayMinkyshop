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
    public class GiaTriConfiguration : IEntityTypeConfiguration<GiaTri>
    {
        public void Configure(EntityTypeBuilder<GiaTri> builder)
        {
            builder.ToTable("GiaTri");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.Ten).HasMaxLength(30).HasDefaultValue(null);

            builder.HasOne(c => c.ThuocTinh).WithMany(c => c.GiaTris).HasForeignKey(c => c.IdThuocTinh).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
