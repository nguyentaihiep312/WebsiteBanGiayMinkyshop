using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    public partial class MinkyShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaiTro = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhomSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdParent = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhomSanPham_NhomSanPham_IdParent",
                        column: x => x.IdParent,
                        principalTable: "NhomSanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViDiem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongDiem = table.Column<float>(type: "real", nullable: false),
                    SoDiemDaDung = table.Column<float>(type: "real", nullable: false),
                    SoDiemDaCong = table.Column<float>(type: "real", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViDiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiGiamGia = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    HinhThucGiamGia = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SoTienCan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    SoTienGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    NgayApDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaoCa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    ThoiGianNhanCa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianGiaoCa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNhanVienTrongCa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNhanVienCaTiepTheo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TienBanDau = table.Column<float>(type: "real", nullable: false),
                    TongTienTrongCa = table.Column<float>(type: "real", nullable: true),
                    TongTienMat = table.Column<float>(type: "real", nullable: true),
                    TongTienKhac = table.Column<float>(type: "real", nullable: true),
                    TienPhatSinh = table.Column<float>(type: "real", nullable: true),
                    GhiChuPhatSinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTienMatCaTruoc = table.Column<float>(type: "real", nullable: true),
                    ThoiGianReset = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongTienMatRut = table.Column<float>(type: "real", nullable: true),
                    GhiChuRutTien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongTienMatCuoiCa = table.Column<float>(type: "real", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoCa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaoCa_NhanVien_IdNhanVienTrongCa",
                        column: x => x.IdNhanVienTrongCa,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdNhomSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://reactnative-examples.com/wp-content/uploads/2022/02/default-loading-image.png"),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_NhomSanPham_IdNhomSanPham",
                        column: x => x.IdNhomSanPham,
                        principalTable: "NhomSanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaTri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaTri_ThuocTinh_IdThuocTinh",
                        column: x => x.IdThuocTinh,
                        principalTable: "ThuocTinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdViDiem = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLanMua = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHang_ViDiem_IdViDiem",
                        column: x => x.IdViDiem,
                        principalTable: "ViDiem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BienThe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GiaBan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    Sku = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://reactnative-examples.com/wp-content/uploads/2022/02/default-loading-image.png"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienThe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BienThe_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinhSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThuocTinhSanPham_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThuocTinhSanPham_ThuocTinh_IdThuocTinh",
                        column: x => x.IdThuocTinh,
                        principalTable: "ThuocTinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayXacNhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHoanThanh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayLayHang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoaiDonHang = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChuCapNhat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienShip = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTien = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThaiGiaoHang = table.Column<int>(type: "int", nullable: false),
                    GhiChuGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherKhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhachHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherKhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherKhachHang_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherKhachHang_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BienTheChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinhSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGiaTri = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienTheChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_GiaTri_IdGiaTri",
                        column: x => x.IdGiaTri,
                        principalTable: "GiaTri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_ThuocTinhSanPham_IdThuocTinhSanPham",
                        column: x => x.IdThuocTinhSanPham,
                        principalTable: "ThuocTinhSanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DonGia = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang_IdGioHang",
                        column: x => x.IdGioHang,
                        principalTable: "GioHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDanhGia = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    BienTheId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGia_BienThe_BienTheId",
                        column: x => x.BienTheId,
                        principalTable: "BienThe",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DanhGia_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KieuThanhToan = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TongTienThanhToan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhThucThanhToan_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TienTruocKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TienSauKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    SoTienGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherLog_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherLog_Voucher_IdVoucher",
                        column: x => x.IdVoucher,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienThe_IdSanPham",
                table: "BienThe",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_BienThe_Sku",
                table: "BienThe",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdBienThe",
                table: "BienTheChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdGiaTri",
                table: "BienTheChiTiet",
                column: "IdGiaTri");

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdThuocTinhSanPham",
                table: "BienTheChiTiet",
                column: "IdThuocTinhSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_BienTheId",
                table: "DanhGia",
                column: "BienTheId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdHoaDon",
                table: "DanhGia",
                column: "IdHoaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdKhachHang",
                table: "DanhGia",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoCa_IdNhanVienTrongCa",
                table: "GiaoCa",
                column: "IdNhanVienTrongCa");

            migrationBuilder.CreateIndex(
                name: "IX_GiaTri_IdThuocTinh",
                table: "GiaTri",
                column: "IdThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IdKhachHang",
                table: "GioHang",
                column: "IdKhachHang",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdBienThe",
                table: "GioHangChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdGioHang",
                table: "GioHangChiTiet",
                column: "IdGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdHoaDon",
                table: "HinhThucThanhToan",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdKhachHang",
                table: "HoaDon",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNhanVien",
                table: "HoaDon",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Ma",
                table: "HoaDon",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IdBienThe",
                table: "HoaDonChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IdHoaDon",
                table: "HoaDonChiTiet",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Email",
                table: "KhachHang",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_IdViDiem",
                table: "KhachHang",
                column: "IdViDiem",
                unique: true,
                filter: "[IdViDiem] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Sdt",
                table: "KhachHang",
                column: "Sdt",
                unique: true,
                filter: "[Sdt] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_Ma",
                table: "NhanVien",
                column: "Ma",
                unique: true,
                filter: "[Ma] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NhomSanPham_IdParent",
                table: "NhomSanPham",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdNhomSanPham",
                table: "SanPham",
                column: "IdNhomSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_Ma",
                table: "SanPham",
                column: "Ma",
                unique: true,
                filter: "[Ma] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdSanPham",
                table: "ThuocTinhSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdThuocTinh",
                table: "ThuocTinhSanPham",
                column: "IdThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Ma",
                table: "Voucher",
                column: "Ma",
                unique: true,
                filter: "[Ma] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherKhachHang_KhachHangId",
                table: "VoucherKhachHang",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherKhachHang_VoucherId",
                table: "VoucherKhachHang",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLog_IdHoaDon",
                table: "VoucherLog",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLog_IdVoucher",
                table: "VoucherLog",
                column: "IdVoucher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienTheChiTiet");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "GiaoCa");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "VoucherKhachHang");

            migrationBuilder.DropTable(
                name: "VoucherLog");

            migrationBuilder.DropTable(
                name: "GiaTri");

            migrationBuilder.DropTable(
                name: "ThuocTinhSanPham");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "BienThe");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "ThuocTinh");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "NhomSanPham");

            migrationBuilder.DropTable(
                name: "ViDiem");
        }
    }
}
