using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MinkyShopProject.Business.Entities;

namespace MinkyShopProject.Business.Context
{
    public class MinkyShopDbContext : DbContext
    {
        public MinkyShopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<SanPham> SanPham { get; set; } = null!;

        public DbSet<NhomSanPham> NhomSanPham { get; set; } = null!;

        public DbSet<BienThe> BienThe { get; set; } = null!;

        public DbSet<GioHang> GioHang { get; set; } = null!;

        public DbSet<GiaoCa> giaoCas { get; set; }

        public DbSet<GioHangChiTiet> GioHangChiTiet { get; set; } = null!;

        public DbSet<ViDiem> ViDiem { get; set; } = null!;

        public DbSet<ThuocTinhSanPham> ThuocTinhSanPham { get; set; } = null!;

        public DbSet<BienTheChiTiet> BienTheChiTiet { get; set; } = null!;

        public DbSet<GiaTri> GiaTri { get; set; } = null!;

        public DbSet<ThuocTinh> ThuocTinh { get; set; } = null!;

        public DbSet<HoaDonChiTiet> HoaDonChiTiet { get; set; } = null!;

        public DbSet<HinhThucThanhToan> HinhThucThanhToan { get; set; } = null!;

        public DbSet<NhanVien> NhanVien { get; set; } = null!;

        public DbSet<DanhGia> DanhGia { get; set; } = null!;

        public DbSet<HoaDon> HoaDon { get; set; } = null!;

        public DbSet<KhachHang> KhachHang { get; set; } = null!;

        public DbSet<Voucher> Voucher { get; set; } = null!;

        public DbSet<VoucherKhachHang> VoucherKhachHang { get; set; } = null!;

        public DbSet<VoucherLog> VoucherLog { get; set; } = null!;
    }
}
