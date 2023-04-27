using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Code = System.Net.HttpStatusCode;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;

namespace MinkyShopProject.Business.Repositories.NhanVien
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly MinkyShopDbContext _context;

        private readonly IConfiguration _configuration;

        public NhanVienRepository(MinkyShopDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Response> ChangeStatus(Guid Id, int status)
        {
            try
            {
                var newStatus = status == 0 ? 1 : 0;
                var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);
                nhanvien.TrangThai = newStatus;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa thành công");
                }

                return new ResponseError(Code.BadRequest, "Sửa thất bại");
            }
            catch (Exception exeption)
            {
                return new ResponseError(Code.InternalServerError, exeption.ToString());
            }
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var nhanvienhoadon = await _context.HoaDon.FirstOrDefaultAsync(c => c.IdNhanVien == Id);
                var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);


                if (nhanvienhoadon != null)
                {
                    return new ResponseError(Code.BadRequest, "Nhân viên này tồn tại hóa đơn không thể xóa");
                }

                if (nhanvien.TrangThai == 1)
                {
                    return new ResponseError(Code.BadRequest, "Không thể xóa nhân viên còn hoạt động");
                }

                _context.NhanVien.Remove(nhanvien);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return new Response(Code.OK, "Xóa thành công");
                }

                return new ResponseError(Code.BadRequest, "Xóa không thành công");
            }
            catch (Exception exeption)
            {
                return new ResponseError(Code.InternalServerError, exeption.ToString());
            }
        }

        public async Task<Response> Get(PaginationRequest paginationRequest)
        {
            try
            {
                var NhanViens = new List<Entities.NhanVien>();
                var pageResult = Convert.ToSingle(paginationRequest.PerPage);
                double pageCount;

                if (paginationRequest.Status == null && paginationRequest.Role == null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Count() / pageResult);

                    NhanViens = await _context.NhanVien
                        .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                        .Take((int)pageResult)
                        .OrderByDescending(c=>c.NgayTao)
                        .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role == null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                        .Where(c => c.TrangThai == paginationRequest.Status)
                        .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                        .Take((int)pageResult)
                        .OrderByDescending(c => c.NgayTao)
                        .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role != null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role == null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.Ten.Contains(paginationRequest.Keyword) || c.DiaChi.Contains(paginationRequest.Keyword) || c.Ma.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.Ten.Contains(paginationRequest.Keyword) || c.DiaChi.Contains(paginationRequest.Keyword) || c.Ma.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role != null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role != null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.VaiTro == paginationRequest.Role && c.Ten.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.VaiTro == paginationRequest.Role && c.Ten.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role == null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }
                else
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword) && c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword) && c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .OrderByDescending(c => c.NgayTao)
                       .ToListAsync();
                }

                var Response = new PaginationResponse<Entities.NhanVien>()
                {
                    Data = NhanViens,
                    CurrentPage = paginationRequest.CurrentPage,
                    Pages = (int)pageCount,
                };

                return new ResponseObject<PaginationResponse<Entities.NhanVien>>(Response, "Lấy dữ liệu thành công");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetById(Guid Id)
        {
            var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);
            return new ResponseObject<Entities.NhanVien>(nhanvien);
        }

        public async Task<Response> Post(Entities.NhanVien NhanVien)
        {
            try
            {
                var nv = await _context.NhanVien.FirstOrDefaultAsync(c => c.Ma == NhanVien.Ma);

                if (nv != null)
                {
                    return new ResponseError(Code.BadRequest, "Mã nhân viên đã tồn tại");
                }

                NhanVien.NgayTao = DateTime.Now;
                _context.NhanVien.Add(NhanVien);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new Response(Code.OK, "Thêm mới thành công");
                }

                return new ResponseError(Code.BadRequest, "Thêm mới thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> Put(Entities.NhanVien NhanVien)
        {
            try
            {
                _context.NhanVien.Update(NhanVien);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa thành công");
                }

                return new ResponseError(Code.BadRequest, "Sửa thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> Login(LoginModels.Login model)
        {
            try
            {
                var NhanVien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Sdt == model.SoDienThoaiOrEmail && c.MatKhau == model.MatKhau || c.Email == model.SoDienThoaiOrEmail && c.MatKhau == model.MatKhau);

                if (NhanVien == null)
                {
                    return new ResponseError(Code.NotFound, "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
                else if (NhanVien.TrangThai == 0)
                {
                    return new ResponseError(Code.BadRequest, "Tài khoản không được quyến sử dụng");
                }
                else if (NhanVien != null)
                {
                    var authenClaims = new List<Claim>
                    {
                        new Claim("Id",NhanVien.Id.ToString()),
                        new Claim("Ten",NhanVien.Ten),
                        new Claim("Anh",NhanVien.Anh),
                        new Claim("VaiTro",NhanVien.VaiTro == 0 ? "0" : "1"),
                        new Claim("TrangThai",NhanVien.TrangThai == 0 ? "0" : "1"),
                    };

                    var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

                    var authenToken = new JwtSecurityToken(
                        issuer: _configuration["Jwt:ValidIssuer"],
                        audience: _configuration["Jwt:ValidateAudience"],
                        expires: DateTime.Now.AddHours(1),
                        claims: authenClaims,
                        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                            authenKey,
                            SecurityAlgorithms.HmacSha512Signature)
                        );

                    var Token = new JwtSecurityTokenHandler().WriteToken(authenToken);

                    return new Response(Code.OK, $"Đăng nhập thành công,{Token}");
                }
                else
                {
                    return new ResponseError(Code.BadRequest, "Đăng nhập thất bại");
                }


            }
            catch (Exception ex)
            {
                return new Response(Code.InternalServerError, ex.ToString());
            }
        }
    }
}
