using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Business.Repositories.NhanVien;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Data;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly INhanVienRepository _Repository;
        private readonly IWebHostEnvironment _env;
        private static string ApiKey = "AIzaSyC1CQ9feCUbui7LV6qId8nesbF9TNma05E";
        private static string Bucket = "imagesangularapp.appspot.com";
        private static string AuthEmail = "truong@gmail.com";
        private static string AuthPassword = "123456";

        public NhanViensController(INhanVienRepository Repository,IWebHostEnvironment env)
        {
            _Repository = Repository;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationRequest paginationRequest)
        {
            return Helper.TransformData(await _Repository.Get(paginationRequest));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Helper.TransformData(await _Repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id,NhanVienModel.NhanVienCreateModel model)
        {
            var NhanVien = new NhanVien()
            {
                Id = id,
                Anh = model.Anh,
                DiaChi = model.DiaChi,
                Email = model.Email,
                GioiTinh = model.GioiTinh,
                Ma = model.Ma,
                MatKhau = model.MatKhau,
                NgaySinh = model.NgaySinh,
                Sdt = model.Sdt,
                Ten = model.Ten,
                TrangThai = model.TrangThai,
                VaiTro = model.VaiTro,
            };

            return Helper.TransformData(await _Repository.Put(NhanVien));
        }

        [HttpPost]
        public async Task<ActionResult> Post(NhanVienModel.NhanVienCreateModel model)
        {
            var NhanVien = new NhanVien()
            {
                Id = Guid.NewGuid(),
                Anh = model.Anh,
                DiaChi = model.DiaChi,
                Email = model.Email,
                GioiTinh = model.GioiTinh,
                Ma = model.Ma,
                MatKhau = model.MatKhau,
                NgaySinh = model.NgaySinh,
                Sdt = model.Sdt,
                Ten = model.Ten,
                TrangThai = model.TrangThai,
                VaiTro = model.VaiTro,
                NgayTao = DateTime.Now
            };

            return Helper.TransformData(await _Repository.Post(NhanVien));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Helper.TransformData(await _Repository.Delete(id));
        }

        [HttpDelete("ChangeStatus/{Id}/{Status}")]
        public async Task<ActionResult> ChangeStatus(Guid Id,int Status)
        {
            return Helper.TransformData(await _Repository.ChangeStatus(Id, Status));
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginModels.Login model)
        {
            return Helper.TransformData(await _Repository.Login(model));
        }
    }
}
