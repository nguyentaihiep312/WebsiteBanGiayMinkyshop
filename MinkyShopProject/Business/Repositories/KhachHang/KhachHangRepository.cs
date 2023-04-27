using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net.Mail;
using System.Net;
using Code = System.Net.HttpStatusCode;

namespace MinkyShopProject.Business.Repositories.KhachHang
{
	public class KhachHangRepository : IKhachHangRepository
	{
		private readonly MinkyShopDbContext _Context;
		private readonly IMapper _Mapper;

		public KhachHangRepository(MinkyShopDbContext context, IMapper mapper)
		{
			_Context = context ?? throw new ArgumentNullException(nameof(context));
			_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<Response> CreateKhachHang(KhachHangThemVaSuaModel model)
		{
			try
			{
				var entityModel = new Entities.KhachHang
				{
					IdViDiem = model.IdViDiem,
					Ma = model.Ma,
					Ten = model.Ten,
					Anh = model.Anh,
					GioiTinh = model.GioiTinh,
					NgaySinh = model.NgaySinh,
					DiaChi = model.DiaChi,
					Sdt = model.Sdt,
					Email = model.Email,
					MatKhau = model.MatKhau,
					NgayTao = DateTime.Now,
					SoLanMua = model.SoLanMua
				};
				await _Context.AddAsync(entityModel);

				var status = await _Context.SaveChangesAsync();

				if (status > 0)
				{
					var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entityModel);
					return new ResponseObject<KhachHangModel>(data, "Thêm thành công");
				}

				return new ResponseError(Code.BadRequest, "Thêm thất bại");
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> UpdateKhachHang(Guid Id, KhachHangThemVaSuaModel model)
		{
			try

			{
				var entityModel = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
				if (entityModel == null)
				{
					return new ResponseError(Code.BadRequest, "không tìm thấy Id khách hàng");
				}
				entityModel.IdViDiem = model.IdViDiem;
				entityModel.Ma = model.Ma;
				entityModel.Ten = model.Ten;
				entityModel.Anh = model.Anh;
				entityModel.GioiTinh = model.GioiTinh;
				entityModel.NgaySinh = model.NgaySinh;
				entityModel.DiaChi = model.DiaChi;
				entityModel.Sdt = model.Sdt;
				entityModel.Email = model.Email;
				entityModel.MatKhau = model.MatKhau;
				entityModel.SoLanMua = model.SoLanMua;

				var status = await _Context.SaveChangesAsync();

				if (status > 0)
				{
					var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entityModel);
					return new ResponseObject<KhachHangModel>(data, "Sửa thành công");
				}
				return new ResponseError(Code.BadRequest, "Sửa thất bại");
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> GetKhachHang(KhachHangQueryModel filter)
		{
			try
			{

				var result = await _Context.KhachHang.Where(c => c.Sdt == filter.Ten || c.Ten.Contains(filter.Ten ?? "")).GetPageAsync(filter);
				var data = _Mapper.Map<Pagination<Entities.KhachHang>, Pagination<KhachHangModel>>(result);
				return new ResponsePagination<KhachHangModel>(data);

			}
			catch (Exception e)
			{
				Log.Error(e, "Lấy dữ liệu bộ phận không thành công");
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> GetbyIdKhachHang(Guid Id)
		{
			try
			{
				var entity = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
				if (entity == null)
				{
					return new ResponseError(Code.BadRequest, "Không tìm thấy Id khách hàng");
				}

				var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entity);
				return new ResponseObject<KhachHangModel>(data);
			}
			catch (Exception e)
			{
				Log.Error(e, "Lấy dữ liệu khách hàng thất bại");
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> DeleteKhachHang(Guid Id)
		{
			try
			{
				var entity = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
				if (entity == null)
				{
					return new ResponseError(Code.BadRequest, "Không tìm thấy Id Khách Hàng");
				}
				_Context.Remove(entity);
				_Context.SaveChanges();

				var tile = entity.Ten;
				return new ResponseDelete(Code.OK, "Xóa thành công", Id, tile);
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> Login(string username, string password)
		{
			try
			{
				var entity = await _Context.KhachHang.AsNoTracking().FirstAsync(c => (c.Sdt == username && c.MatKhau == password) || (c.Email == username && c.MatKhau == password));
				if (entity == null)
				{
					return new ResponseError(Code.BadRequest, "Thất bại");
				}
				return new ResponseObject<KhachHangModel>(_Mapper.Map<Entities.KhachHang, KhachHangModel>(entity));
			}
			catch (Exception e)
			{
				Log.Error(e, string.Empty);
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}

		public async Task<Response> ForgotPassword(string email)
		{
			try
			{
				var khachHang = _Context.KhachHang.FirstOrDefault(c => c.Email == email);
				if (khachHang != null)
				{
					var pass = Guid.NewGuid().ToString();
					using (MailMessage mail = new MailMessage())
					{
						mail.From = new MailAddress("minkyshop@fpt.edu.vn");
						mail.To.Add(email);
						mail.Subject = "Quên mật khẩu";
						mail.Body = "Mật khẩu của bạn là: " + pass;

						using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
						{
							smtp.Credentials = new NetworkCredential("kem15122002@gmail.com", "ohefwclzhmgxstsd");
							smtp.EnableSsl = true;
							smtp.Send(mail);
						}
						khachHang.MatKhau = pass;
						await _Context.SaveChangesAsync();
					}
				}
				else
				{
					return new ResponseError(Code.NotFound, "Không tìm thấy");
				}

				return new Response(HttpStatusCode.OK, "Vui Lòng Check Mail Của Bạn");
			}
			catch (Exception e)
			{
				return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
			}
		}
	}
}
