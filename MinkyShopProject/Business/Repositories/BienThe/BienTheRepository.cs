using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Common;
using System.Net;
using Serilog;
using System.Linq;

namespace MinkyShopProject.Business.Repositories.BienThe
{
	public class BienTheRepository : IBienTheRepository
	{
		private readonly MinkyShopDbContext _context;
		private readonly IMapper _mapper;

		public BienTheRepository(MinkyShopDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Response> AddAsync(BienTheCreateModel obj)
		{
			try
			{
				if (_context.SanPham.FirstOrDefault(c => c.Ten.ToLower().Trim() == obj.SanPham.Ten.ToLower().Trim()) != null)
				{
					return new ResponseError(HttpStatusCode.BadRequest, "Sản Phẩm Đã Tồn Tại");
				}

				var skus = new List<string[]>();

				var idBienThe = Guid.NewGuid();

				var idSanPham = obj.SanPham.Id;

				var idThuocTinhSanPham = Guid.NewGuid();

				await _context.SanPham.AddAsync(new Entities.SanPham() { Id = idSanPham, Ma = "SP" + Helper.RandomString(5), NgayTao = DateTime.Now, IdNhomSanPham = obj.SanPham?.IdNhomSanPham, Ten = obj.SanPham?.Ten });

				if (obj.ThuocTinhs != null)
				{
					obj.ThuocTinhs.OrderBy(c => c.Id);
					foreach (var x in obj.ThuocTinhs)
					{
						var thuocTinhTonTai = _context.ThuocTinh.AsNoTracking().FirstOrDefault(c => c.Ten.ToLower().Trim().Equals(x.Ten.ToLower().Trim()));
						if (thuocTinhTonTai != null)
						{
							x.Id = thuocTinhTonTai.Id;
						}
						else
						{
							if (x.Id == Guid.Empty)
							{
								x.Id = Guid.NewGuid();
								await _context.ThuocTinh.AddAsync(new Entities.ThuocTinh { Id = x.Id, Ten = x.Ten, NgayTao = DateTime.Now });
							}
						}

						var thuocTinhSanPhamExist = await _context.ThuocTinhSanPham.FirstOrDefaultAsync(c => c.IdSanPham == idSanPham && c.IdThuocTinh == x.Id);

						// Thêm Thuộc Tính Cho Sản Phẩm
						if (thuocTinhSanPhamExist == null)
						{
							var thuocTinhSanPham = new ThuocTinhSanPham() { Id = idThuocTinhSanPham, IdSanPham = idSanPham, IdThuocTinh = x.Id };
							await _context.ThuocTinhSanPham.AddAsync(thuocTinhSanPham);
						}
						else
						{
							idThuocTinhSanPham = thuocTinhSanPhamExist.Id;
						}

						// Tạo Ra Các Biến Giá Trị Sku Cho Biến Thể
						var sku = new List<string>();

						if (x.GiaTris != null)
						{
							foreach (var y in x.GiaTris)
							{
								var giaTriTonTai = _context.GiaTri.AsNoTracking().FirstOrDefault(c => c.Ten.ToLower().Trim().Contains(y.Ten.ToLower().Trim()));
								if (giaTriTonTai != null)
								{
									y.Id = giaTriTonTai.Id;
								}
								else
								{
									if (y.Id == Guid.Empty)
									{
										y.Id = Guid.NewGuid();
										await _context.GiaTri.AddAsync(new GiaTri { Id = y.Id, Ten = y.Ten, IdThuocTinh = x.Id, TrangThai = true });
									}
								}

								// Nếu Tên Thuộc Tính Có 2 Kí Tự Trở Lên Thì Lấy 2 Ký Tự Không Thì Chỉ Lấy Kí Tự Đầu Tiên
								var thuocTinh = x.Ten.RemoveSpecialCharacters().Split(" ");

								var tenThuocTinh = thuocTinh?.Select(c => string.IsNullOrEmpty(c) ? null : c.Length >= 2 ? c.Substring(0, 2) : c.Substring(0, 1));

								// Lấy Kí Tự Đầu Tiên
								var giaTri = y.Ten.RemoveSpecialCharacters().Split(" ");

								var tenGiaTri = giaTri?.Select(c => string.IsNullOrEmpty(c) ? null : c.Length >= 2 ? c.Substring(0, 2) : c.Substring(0, 1));

								if (tenThuocTinh != null && tenGiaTri != null)
									sku.Add(string.Join("", tenThuocTinh) + string.Join("", tenGiaTri) + "/" + y.Id + "/" + x.Id + "/" + idThuocTinhSanPham);
							}
						}

						skus.Add(sku.ToArray());
						idThuocTinhSanPham = Guid.NewGuid();
					}

					// Tổ Hợp Các Trường Hợp Từ Các Giá Trị
					foreach (var x in skus.CartesianProduct())
					{
						var tenSanPham = obj.SanPham?.Ten?.Split(" ").Select(c => string.IsNullOrEmpty(c) ? null : c.Substring(0, 1));
						// Mỗi Giá Trị X Sẽ Là Một Biến Thể
						var bienThe = new Entities.BienThe() { Id = idBienThe, SoLuong = 0, GiaBan = 0, IdSanPham = idSanPham, Sku = String.Join("", tenSanPham) + String.Join("", x.Select(c => c.Split("/")[0])) };
						await _context.BienThe.AddAsync(bienThe);

						foreach (var y in x)
						{
							var sku = y.Split("/"); // sku[0] : sku_id, sku[1] : Id Giá Trị, sku[2] : Id Thuộc Tính, sku[3] : Id Thuộc Tính Sản Phẩm

							// Tạo Ra Biến Thể Chi Tiết Của Từng Biến Thể
							var bienTheChiTiet = new BienTheChiTiet() { IdGiaTri = Guid.Parse(sku[1]), IdThuocTinhSanPham = Guid.Parse(sku[3]), IdBienThe = idBienThe };
							await _context.BienTheChiTiet.AddAsync(bienTheChiTiet);
						}

						idBienThe = Guid.NewGuid();
					}

					var status = await _context.SaveChangesAsync();

					if (status > 0)
					{
						var data = _mapper.Map<BienTheCreateModel, BienTheModel>(obj);
						return new ResponseObject<BienTheModel>(data, "Thêm thành công");
					}
				}

				return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> DeleteAsync(Guid id)
		{
			try
			{
				var bienThe = await _context.BienThe.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

				if (bienThe == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

				_context.BienThe.Remove(bienThe);

				var status = await _context.SaveChangesAsync();

				if (status > 0)
				{
					return new ResponseError(HttpStatusCode.OK, "Xóa thành công");
				}

				return new ResponseError(HttpStatusCode.BadRequest, "Xóa Thất Bại");
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> FindAsync(Guid id)
		{
			#region ThuocTinhs
			var thuocTinhs = from tt in _context.ThuocTinh
							 join gt in _context.GiaTri on tt.Id equals gt.IdThuocTinh
							 join ttsp in _context.ThuocTinhSanPham on tt.Id equals ttsp.IdThuocTinh
							 join sp in _context.SanPham on ttsp.IdSanPham equals sp.Id
							 join btct in _context.BienTheChiTiet on new { gt = gt.Id, ttsp = ttsp.Id } equals new { gt = btct.IdGiaTri, ttsp = btct.IdThuocTinhSanPham }
							 join bt in _context.BienThe on btct.IdBienThe equals bt.Id
							 orderby tt.Id
							 where sp.Id == id
							 group new { tt, gt } by new
							 {
								 tt.Id,
								 tt.NgayTao,
								 tt.TrangThai,
								 tt.Ten,
								 gt.IdThuocTinh,
							 } into ttc
							 select new ThuocTinhModel
							 {
								 Id = ttc.First().tt.Id,
								 Ten = ttc.First().tt.Ten,
								 GiaTris = _mapper.Map<List<GiaTri>, List<GiaTriModel>>(ttc.Select(c => c.gt).Distinct().ToList())
							 };
			#endregion

			var sanPham = await _context.SanPham.AsNoTracking().Include(c => c.BienThes).ThenInclude(c => c.BienTheChiTiets).ThenInclude(c => c.GiaTri).AsNoTracking().Include(c => c.NhomSanPham).ThenInclude(c => c.NhomSanPhamEntity).AsNoTracking().FirstAsync(c => c.Id == id);

			var bienTheChiTietModel = new BienTheChiTietModel() { ThuocTinhs = thuocTinhs.ToList(), SanPham = _mapper.Map<Entities.SanPham, SanPhamModel>(sanPham) };

			return new ResponseObject<BienTheChiTietModel>(bienTheChiTietModel);
		}

		public async Task<Response> FindAsync(string ma)
		{
			try
			{
				return new ResponseObject<BienTheModel>(_mapper.Map<Entities.BienThe, BienTheModel>(await _context.BienThe.AsNoTracking().Include(c => c.BienTheChiTiets).ThenInclude(c => c.GiaTri).Include(c => c.SanPham).FirstAsync(c => c.Sku == ma)));
			}
			catch (Exception e)
			{
				Log.Error(e, "Lấy dữ liệu thất bại");
				return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
			}
		}

		public async Task<Response> UpdateAsync(Guid id, BienTheModel obj)
		{
			try
			{
				if (obj == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Lỗi rồi");

				var bienThe = _context.BienThe.AsNoTracking().FirstOrDefault(c => c.Id == id);

				if (bienThe == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

				bienThe.TrangThai = obj.TrangThai;

				bienThe.Anh = obj.Anh;

				bienThe.GiaBan = obj.GiaBan;

				_context.BienThe.Update(_mapper.Map<BienTheModel, Entities.BienThe>(obj));

				var status = await _context.SaveChangesAsync();

				if (status > 0)
				{
					var data = _mapper.Map<Entities.BienThe, BienTheModel>(bienThe);
					return new ResponseObject<BienTheModel>(data, "Cập nhật thành công");
				}
				return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
