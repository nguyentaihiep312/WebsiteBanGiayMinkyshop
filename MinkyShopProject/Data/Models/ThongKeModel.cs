namespace MinkyShopProject.Data.Models
{
	public class SoLuongThangNam
	{
		public int nam { get; set; }
		public int thang { get; set; }
		public int soluong { get; set; }
	}
	public class TongTienNgayTienThangNam
	{
		public float TongTien { get; set; }
		public int ngay { get; set; }
		public int thang { get; set; }
		public int nam { get; set; }

	}
	public class ThongKeTongTienResult
	{
		public float TongTienTatCa { get; set; }
		public int TongDonHuy { get; set; }
		public int TongDonBan { get; set; }
		public int DonQuay { get; set; }
		public int DonOnline { get; set; }
		public List<TongTienNgayTienThangNam> ThongKeTheoNgayTienThangNam { get; set; }
	}
	public class NhanVienBanHangNhieuNHat
	{
		public Guid Id { get; set; }
		public string Ten { get; set; }
		public int SoHoaDon { get; set; }
		public float TongTien { get; set; }
		public string sdt { get; set; }
		public DateTime ngayvaolam { get; set; }
	}
	public class NhanVienKhongBanHang
	{
		public string Ten { get; set; }
		public int SoHoaDon { get; set; }
		public string sdt { get; set; }
		public float TongTien { get; set; }
		public DateTime ngayvaolam { get; set; }
	}
	public class KhachHangMuaNhieuNhat
	{
		public string Ten { get; set; }
		public string sdt { get; set; }
		public int SoHoaDon { get; set; }
		public float TongTien { get; set; }

	}

	public class KhachHangHuyHoaDon
	{
		public string Ten { get; set; }
		public string sdt { get; set; }
		public int SoHoaDon { get; set; }
	}

	public class SpBanNhieuNHatSoLuong
	{
		public string Ten { get; set; }
		public int SoLuong { get; set; }

	}

	public class SpBanNhieuNHatTien
	{
		public string Ten { get; set; }
		public float Tien { get; set; }

	}
}
