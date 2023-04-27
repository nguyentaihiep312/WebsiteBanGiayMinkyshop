using MinkyShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
	public class DanhGiaModel
	{
		public Guid Id { get; set; }

		public Guid IdKhachHang { get; set; }

		public Guid IdHoaDon { get; set; }

		public int SoDanhGia { get; set; }

		public DateTime NgayDanhGia { get; set; }

		public string? Anh { get; set; } = null!;

		public string NoiDung { get; set; } = null!;

		public int TrangThai { get; set; }

		public HoaDonModel? HoaDon { get; set; }
	}

	public class DanhGiaCreateModel
	{
		public Guid Id { get; set; }

		public Guid IdBienThe { get; set; }

		public Guid IdKhachHang { get; set; }

		public Guid IdHoaDon { get; set; }

		public int SoDanhGia { get; set; }

		public DateTime NgayDanhGia { get; set; } = DateTime.Now;

		public string? Anh { get; set; } = "1";

		public string NoiDung { get; set; } = null!;

		public int TrangThai { get; set; }
	}

	public class DanhGiaQueryModel : PaginationRequest
	{

	}
}
