using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Code = System.Net.HttpStatusCode;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MinkyShopProject.Business.Pagination;
using AutoMapper;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace MinkyShopProject.Business.Repositories.GiaoCa
{
    public class GiaoCaRepositories : IGiaoCaRepositories
    {

        private readonly MinkyShopDbContext _context;

        public GiaoCaRepositories(MinkyShopDbContext context)
        {
            _context = context;
        }

        public async Task<Response> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model)
        {
            try
            {
                var ca = new Entities.GiaoCa()
                {
                    Id = Guid.NewGuid(),
                    IdNhanVienTrongCa = model.IdNhanVienTrongCa,
                    ThoiGianNhanCa = model.ThoiGianNhanCa,
                    TienBanDau = model.TienBanDau,
                    TongTienMat = model.TongTienMat,
                    TrangThai = model.TrangThai,
                    GhiChuPhatSinh = ""
                };

                await _context.AddAsync(ca);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.Created, "Bạn đã bắt đầu ca làm việc");
                }

                return new ResponseError(Code.BadRequest, "Bắt đầu ca làm việc thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaHienTai(Guid Id, DateTime ThoiGian)
        {
              var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa == Id && c.ThoiGianNhanCa.Day == ThoiGian.Day && c.ThoiGianNhanCa.Month == ThoiGian.Month && c.ThoiGianNhanCa.Year == ThoiGian.Year && c.ThoiGianNhanCa.Hour <= ThoiGian.Hour && c.TrangThai == 0);
              return new Response<Entities.GiaoCa>(Code.OK,Ca);
        }

        public async Task<Response> KetCa(Guid Id,GiaoCaModels.GiaoCaEditModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c=>c.Id == Id);
                Ca.ThoiGianGiaoCa = model.ThoiGianGiaoCa;
                Ca.IdNhanVienCaTiepTheo = model.IdNhanVienCaTiepTheo;
                Ca.TongTienTrongCa = model.TongTienTrongCa;
                Ca.TongTienMat = model.TongTienMat;
                Ca.TongTienKhac = model.TongTienKhac;
                Ca.TienPhatSinh = model.TienPhatSinh;
                Ca.GhiChuPhatSinh = model.GhiChuPhatSinh;
                Ca.TongTienMatCaTruoc = model.TongTienMatCaTruoc;
                Ca.TongTienMatCuoiCa = model.TongTienMatCuoiCa;
                if (Ca.TongTienMatRut == null)
                {
                    Ca.TongTienMatRut = model.TongTienMatRut;
                }
                else
                {
                    Ca.TongTienMatRut += model.TongTienMatRut;
                }
                Ca.GhiChuRutTien += model.GhiChuRutTien;
                Ca.TrangThai = model.IdNhanVienCaTiepTheo == null ? 3 : 1;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    await SendMail(Id);
                    return new Response(Code.OK, "Hoàn Thành Báo Cáo");
                }
                else
                {
                    return new ResponseError(Code.BadRequest, "Kết Ca Thất Bại,Vui Lòng Thử Lại");
                }
            }
            catch (Exception ex)
            {

                return new ResponseError(Code.InternalServerError, ex.ToString());
            }
        }

        public async Task<Response> GetHoaDonCa(Guid IdNhanVien,DateTime ThoiGian)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa
                                                                == IdNhanVien && c.ThoiGianNhanCa.Day
                                                                == ThoiGian.Day && c.ThoiGianNhanCa.Month
                                                                == ThoiGian.Month && c.ThoiGianNhanCa.Year
                                                                == ThoiGian.Year && c.ThoiGianNhanCa.Hour
                                                                <= ThoiGian.Hour && c.TrangThai == 0);

            var hoadons = await _context.HoaDon.Where(c => c.NgayTao.Day == Ca.ThoiGianNhanCa.Day 
                                                        && c.NgayTao.Month == Ca.ThoiGianNhanCa.Month 
                                                        && c.NgayTao.Year == Ca.ThoiGianNhanCa.Year 
                                                        && c.TrangThai == 0 || c.TrangThaiGiaoHang == 3
                                                        && c.NgayTao.Hour >= Ca.ThoiGianNhanCa.Hour).ToListAsync();
            return new Response<List<Entities.HoaDon>>(Code.OK, hoadons);
        }

        public async Task<Response> GetHoaDonCaDaKetThuc(Guid IdNhanVien, DateTime ThoiGian)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa
                                                                == IdNhanVien && c.ThoiGianNhanCa.Day
                                                                == ThoiGian.Day && c.ThoiGianNhanCa.Month
                                                                == ThoiGian.Month && c.ThoiGianNhanCa.Year
                                                                == ThoiGian.Year && c.TrangThai == 0 || c.TrangThai == 1 || c.TrangThai == 2 || c.TrangThai == 3);

            var hoadons = await _context.HoaDon.Where(c => c.NgayTao.Day == Ca.ThoiGianNhanCa.Day
                                                        && c.NgayTao.Month == Ca.ThoiGianNhanCa.Month
                                                        && c.NgayTao.Year == Ca.ThoiGianNhanCa.Year
                                                        && c.TrangThai == 0 || c.TrangThaiGiaoHang == 3
                                                        && c.NgayTao.Hour >= Ca.ThoiGianNhanCa.Hour).ToListAsync();
            return new Response<List<Entities.HoaDon>>(Code.OK, hoadons);
        }

        public async Task<Response> GetTienMatHoaDon(Guid IdHoaDon)
        {
            var hinhthucthanhtoan = await _context.HinhThucThanhToan.Where(c => c.IdHoaDon == IdHoaDon && c.KieuThanhToan == 0).ToListAsync();
            return new Response<List<HinhThucThanhToan>>(Code.OK, hinhthucthanhtoan);
        }

        public async Task<Response> GetTienChuyenKhoanHoaDon(Guid IdHoaDon)
        {
            var hinhthucthanhtoan = await _context.HinhThucThanhToan.Where(c => c.IdHoaDon == IdHoaDon && c.KieuThanhToan == 1).ToListAsync();
            return new Response<List<HinhThucThanhToan>>(Code.OK, hinhthucthanhtoan);
        }

        public async Task<Response> UpdateTiemMat(Guid IdCa, float TongTien)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == IdCa);
                Ca.TongTienMat = Ca.TongTienMat += TongTien;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "");
                }
                else
                {
                    return new Response(Code.BadRequest, "Error");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCa(GiaoCaModels.GiaoCaQueryModel query)
        {
            try
            {
                var Cas = new List<Entities.GiaoCa>();
                var pageResult = Convert.ToSingle(query.PerPage);
                double pageCount = 0;

                if (query.Status == null && query.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.giaoCas.Count() / pageResult);

                    Cas = _context.giaoCas
                       .Skip((query.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c=>c.ThoiGianNhanCa)
                       .ToList();
                }
                else if (query.Status == null && query.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.giaoCas.Where(c=>c.NhanVien.Ten.Contains(query.Keyword)).Count() / pageResult);

                    Cas = _context.giaoCas
                       .Where(c => c.NhanVien.Ten.Contains(query.Keyword))
                       .Skip((query.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.ThoiGianNhanCa)
                       .ToList();
                }
                else if (query.Status != null && query.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.giaoCas.Where(c=>c.TrangThai == query.Status).Count() / pageResult);

                    Cas = _context.giaoCas
                       .Where(c => c.TrangThai == query.Status)
                       .Skip((query.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.ThoiGianNhanCa)
                       .ToList();
                }
                else
                {
                    pageCount = Math.Ceiling(_context.giaoCas.Where(c => c.TrangThai == query.Status && c.NhanVien.Ten.Contains(query.Keyword)).Count() / pageResult);

                    Cas = _context.giaoCas
                       .Where(c => c.TrangThai == query.Status)
                       .Skip((query.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.ThoiGianNhanCa)
                       .ToList();
                }


                var Response = new PaginationResponse<Entities.GiaoCa>()
                {
                    Data = Cas,
                    CurrentPage = query.CurrentPage,
                    Pages = (int)pageCount,
                };

                return new ResponseObject<PaginationResponse<Entities.GiaoCa>>(Response, "Lấy dữ liệu thành công");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaDuocChon(Guid Id)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
            return new Response<Entities.GiaoCa>(Code.OK, Ca);
        }

        public async Task<Response> RutTien(Guid Id, GiaoCaModels.ResetTienModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.ThoiGianReset = model.ThoiGianReset;
                if (Ca.TongTienMatRut == null)
                {
                    Ca.TongTienMatRut = model.TongTienMatRut;
                }
                else
                {
                    Ca.TongTienMatRut += model.TongTienMatRut;
                }
                Ca.GhiChuRutTien += model.GhiChuRutTien;
                Ca.TongTienMat = Ca.TongTienMat - model.TongTienMatRut;
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new Response(Code.OK, "Rút Tiền Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Rút Tiền Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> UpdateNhanVien(Guid Id, GiaoCaModels.GiaoCaEditModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.IdNhanVienCaTiepTheo = model.IdNhanVienCaTiepTheo;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Sửa Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaTruoc(Guid IdNhanVien)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienCaTiepTheo == IdNhanVien && c.ThoiGianNhanCa.Day == DateTime.Now.Day && c.ThoiGianNhanCa.Month == DateTime.Now.Month && c.ThoiGianNhanCa.Year == DateTime.Now.Year && c.ThoiGianNhanCa.Hour <= DateTime.Now.Hour && c.TrangThai == 1);
            return new Response<Entities.GiaoCa>(Code.OK, Ca);
        }

        public async Task<Response> NhanCa(Guid Id)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.TrangThai = 2;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Sửa Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> SendMail(Guid IdCa)
        {
            try
            {
                var ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == IdCa);
                var nhanvienbangiao = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == ca.IdNhanVienTrongCa);
                var nhanviennhanca = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == ca.IdNhanVienCaTiepTheo);

                var Body = "<div class='card'>";
                Body += "<div>";
                Body += "<div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Nhân viên bàn giao: {nhanvienbangiao.Ma} - {nhanvienbangiao.Ten}</label>";
                Body += "</div>";
                if (nhanviennhanca != null)
                {
                    Body += "<div>";
                    Body += $"<label class='form-control-label'>Nhân viên chận ca: {nhanviennhanca.Ma} - {nhanviennhanca.Ten}</label>";
                    Body += "</div>";
                }
                Body += "<div>";
                Body += $"<label class='form-control-label'>Thời gian nhận ca: {ca.ThoiGianNhanCa}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Thời gian giao ca: {ca.ThoiGianGiaoCa}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Tiền đầu ca: {ca.TienBanDau}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Tổng tiền mặt trong ca: {ca.TongTienMat}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Tổng tiền mặt rút trong ca: {ca.TongTienMatRut}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Tiền Phát sinh : {ca.TienPhatSinh}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Ghi chú: {ca.GhiChuPhatSinh}</label>";
                Body += "</div>";
                Body += "<div>";
                Body += $"<label class='form-control-label'>Tổng tiền mặt cuối ca: {ca.TongTienMatCuoiCa}</label>";
                Body += "</div>";
                Body += "</div>";
                Body += "</div>";
                Body += "</div>";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("minkyshop@fpt.edu.vn");
                    mail.To.Add("txuantruong400@gmail.com");
                    mail.Subject = $"BÁO CÁO KẾT CA - NHÂN VIÊN: {nhanvienbangiao.Ma} - {nhanvienbangiao.Ten}";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("kem15122002@gmail.com", "ohefwclzhmgxstsd");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return new Response(Code.OK, "Vui Lòng Check Mail Của Bạn");
            }
            catch (Exception e)
            {
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> CaDangCho()
        {
            var Cas = await _context.giaoCas.Where(a => a.ThoiGianNhanCa.Day == DateTime.Now.Day && a.ThoiGianNhanCa.Month == DateTime.Now.Month && a.ThoiGianNhanCa.Year == DateTime.Now.Year).ToListAsync();
            return new Response<List<Entities.GiaoCa>>(Code.OK, Cas);
        }
    }
}
