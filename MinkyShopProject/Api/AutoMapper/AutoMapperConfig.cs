using AutoMapper;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Thuộc tính
            CreateMap<ThuocTinh, ThuocTinhModel>().ReverseMap();

            CreateMap<Pagination<ThuocTinh>, Pagination<ThuocTinhModel>>().ReverseMap();

            // Nhóm sản phẩm
            CreateMap<NhomSanPham, NhomSanPhamModel>().ReverseMap();

            CreateMap<Pagination<NhomSanPham>, Pagination<NhomSanPhamModel>>().ReverseMap();

            // Khách hàng
            CreateMap<KhachHang, KhachHangModel>().ReverseMap();

            CreateMap<Pagination<KhachHang>, Pagination<KhachHangModel>>().ReverseMap();

            // Sản Phẩm
            CreateMap<SanPham, SanPhamModel>().ReverseMap();

            CreateMap<Voucher, VoucherModel>().ReverseMap();

            CreateMap<Pagination<Voucher>, Pagination<VoucherModel>>().ReverseMap();

            CreateMap<Voucher, VoucherCreateModel>().ReverseMap();

            CreateMap<Pagination<SanPham>, Pagination<SanPhamModel>>().ReverseMap();

            // Hóa đơn
            CreateMap<HoaDonModel, HoaDon>().ReverseMap();

            CreateMap<HoaDonCreateModel, HoaDon>().ForMember(c => c.KhachHang, c => c.Ignore()).ForMember(c => c.NhanVien, c => c.Ignore()).ForMember(c => c.DanhGia, c => c.Ignore()).ReverseMap();

            CreateMap<Pagination<HoaDon>, Pagination<HoaDonModel>>().ReverseMap();

            // Hóa đơn chi tiết
            CreateMap<HoaDonChiTietModel, HoaDonChiTiet>().ForMember(c => c.BienThe, c => c.Ignore()).ReverseMap();

            // Hình thức thanh toán
            CreateMap<HinhThucThanhToan, HinhThucThanhToanModel>().ReverseMap();

            // Giá trị
            CreateMap<GiaTri, GiaTriModel>().ReverseMap();

            // Nhân viên
            CreateMap<NhanVien, NhanVienModel.NhanVienCreateModel>().ReverseMap();

            // Biến thể

            CreateMap<BienTheCreateModel, BienThe>().ReverseMap();

            CreateMap<BienTheCreateModel, BienTheModel>().ReverseMap();

            CreateMap<BienTheModel, BienThe>().ReverseMap();

            CreateMap<BienTheChiTietModelGet, BienTheChiTiet>().ReverseMap();

            // Giỏ hàng
            CreateMap<GioHang, GioHangModel>().ReverseMap();

            CreateMap<GioHangChiTiet, GioHangChiTietModel>().ReverseMap();

            CreateMap<Pagination<GioHang>, Pagination<GioHangModel>>().ReverseMap();

            // Ví điểm
            CreateMap<ViDiem, ViDiemModel>().ReverseMap();

            // Voucher
            CreateMap<Voucher, VoucherModel>().ReverseMap();

            CreateMap<Pagination<Voucher>, VoucherModel>().ReverseMap();

            CreateMap<VoucherKhachHang, VoucherKhachHangModel>().ReverseMap();

            CreateMap<VoucherLog, VoucherLogModel>().ForMember(c => c.Voucher, c => c.Ignore()).ReverseMap();

			// Đánh giá
			CreateMap<DanhGia, DanhGiaModel>().ReverseMap();

			CreateMap<DanhGiaCreateModel, DanhGia>().ForMember(c => c.HoaDon, c => c.Ignore()).ForMember(c => c.KhachHang, c => c.Ignore()).ReverseMap();

			CreateMap<Pagination<DanhGia>, Pagination<DanhGiaModel>>().ReverseMap();

            //Giao Ca
            CreateMap<Pagination<GiaoCaModels.GiaoCaModel>, Pagination<GiaoCa>>().ReverseMap();
		}
    }
}
