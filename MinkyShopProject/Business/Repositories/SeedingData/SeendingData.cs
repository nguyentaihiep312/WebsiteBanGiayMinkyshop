using Bogus;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using static System.Net.WebRequestMethods;

namespace MinkyShopProject.Business.Repositories.SeedingData
{
    public class SeendingData
    {
        private readonly MinkyShopDbContext _Context;

        public SeendingData(MinkyShopDbContext Context)
        {
            _Context = Context;
        }
        private List<Guid> ListGuid(int n)
        {
            var Guids = new List<Guid>();
            while (Guids.Count < n)
            {
                var newGuid = Guid.NewGuid();
                if (!Guids.Contains(newGuid))
                    Guids.Add(newGuid);
            }
            return Guids;
        }
        public Faker faker = new Faker();
        public async Task SeendingviDiem()
        {
            int soluong = 30;
            var ListId = ListGuid(soluong);
            for (int i = 0; i < soluong; i++)
            {
                _Context.ViDiem.Add(new Entities.ViDiem()
                {
                    Id = ListId[i],
                    SoDiemDaCong = faker.Random.Int(min: 100, max: 1000),
                    SoDiemDaDung = faker.Random.Int(min: 100, max: 1000),
                    TongDiem = faker.Random.Int(min: 100, max: 1000),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                }
                );
            }
            await _Context.SaveChangesAsync();
        }
        public async Task SeeddingNhanVien()
        {
            var lst = new List<Entities.NhanVien>()
            {
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Admin",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(3).jpg?alt=media&token=eba414e6-ab2d-4360-b6a3-3b6dd0988072",
                    DiaChi = "Tuyên Quang",
                    Email = "admin@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Admin@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 0,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Viết Hải Đăng",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2FTaeyang-1.jpeg?alt=media&token=ad56db35-11e8-465a-87ce-7882274f80a3",
                    DiaChi = "Hà Nội",
                    Email = "dang@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Dang@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Quyết Thắng",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(1).jpg?alt=media&token=e20431c5-b05b-4be9-92c5-7937a3ff1220",
                    DiaChi = "Hà Nội",
                    Email = "thang@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Thang@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Tài Hiệp",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(2).jpg?alt=media&token=1836b026-60f1-4fbe-8ee5-c8d008b77a2c",
                    DiaChi = "Hà Nội",
                    Email = "hiep@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Hiep@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Văn Đạt",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng.jpg?alt=media&token=6a4e213f-dda2-4234-bd1c-18965e9479ac",
                    DiaChi = "Hà Nội",
                    Email = "dat@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Dat@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Trần Văn Trường",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Fg-dragon.jpg?alt=media&token=a33eb3b2-d6e8-4094-b145-3ea7c1893709",
                    DiaChi = "Hà Nội",
                    Email = "truong@gmail.com",
                    GioiTinh = true,
                    MatKhau = "Truong@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Huyền Trang",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(4).jpg?alt=media&token=4255cfad-8008-4401-9f04-86cd4ff4878c",
                    DiaChi = "Hà Nội",
                    Email = "trang@gmail.com",
                    GioiTinh = false,
                    MatKhau = "Trang@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 0,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Thị Hương",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(5).jpg?alt=media&token=a3e6705f-e56d-40a9-973b-77284743cc24",
                    DiaChi = "Hà Nội",
                    Email = "huong@gmail.com",
                    GioiTinh = false,
                    MatKhau = "Huong@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 0,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Thị Phương",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(6).jpg?alt=media&token=7a8ecd8f-d4a3-4b50-b323-a0c671e6f3e0",
                    DiaChi = "Hà Nội",
                    Email = "phuong@gmail.com",
                    GioiTinh = false,
                    MatKhau = "Phuong@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 0,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                {
                    Id = Guid.NewGuid(),
                    Ma = $"NV{Helper.RandomNumber(5)}",
                    Ten = "Nguyễn Ngọc Ánh",
                    Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Ft%E1%BA%A3i%20xu%E1%BB%91ng%20(7).jpg?alt=media&token=84dc97ca-290e-48f0-8049-b26f1f1a302e",
                    DiaChi = "Hà Nội",
                    Email = "Anh@gmail.com",
                    GioiTinh = false,
                    MatKhau = "Anh@12",
                    NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                    NgayTao = DateTime.Now,
                    Sdt = Helper.RandomNumber(10),
                    TrangThai = 1,
                    VaiTro = 1,
                },
                new Entities.NhanVien()
                 {
                     Id = Guid.NewGuid(),
                     Ma = $"NV{Helper.RandomNumber(5)}",
                     Ten = "Nguyễn Quỳnh Hương",
                     Anh = "https://firebasestorage.googleapis.com/v0/b/imagesangularapp.appspot.com/o/images%2Fimages.jpg?alt=media&token=2053cd19-cf35-4c09-a82c-4e0a0c19559d",
                     DiaChi = "Hà Nội",
                     Email = "huong@gmail.com",
                     GioiTinh = false,
                     MatKhau = "Huong@12",
                     NgaySinh = DateTime.Parse("04/25/2002 07:20:52"),
                     NgayTao = DateTime.Now,
                     Sdt = Helper.RandomNumber(10),
                     TrangThai = 1,
                     VaiTro = 1,
                 },
            };
            _Context.NhanVien.AddRange(lst);
            await _Context.SaveChangesAsync();
        }

        public async Task SeeddingSanPham()
        {
            int soluong = 70;
            var ListId = ListGuid(soluong);
            for (int i = 0; i < soluong; i++)
            {
                _Context.SanPham.Add(new Entities.SanPham()
                {
                    Id = ListId[i],
                    Ten = faker.Name.LastName(),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                    Ma = "SP" + faker.Random.String2(4, 4, "QEWRTYUIOPLKJHGFDSAZXCVBNM") + faker.Random.Number(0, 9),
                    Anh = faker.Image.LoremFlickrUrl(),
                });
            }
            await _Context.SaveChangesAsync();
        }

        public async Task SeeddingBienThe()
        {
            int soluong = 140;
            var ListId = ListGuid(soluong);
            var IdSanPham = _Context.SanPham.Select(x => x.Id).ToList();
            for (int i = 0; i < soluong; i++)
            {
                _Context.BienThe.Add(new Entities.BienThe()
                {
                    Id = ListId[i],
                    IdSanPham = faker.PickRandom(IdSanPham),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                    SoLuong = faker.Random.Number(1, 100),
                    GiaBan = faker.Random.Number(1, 100),
                    Anh = "https://reactnative-examples.com/wp-content/uploads/2022/02/default-loading-image.png",
                    Sku = faker.Random.String2(4, 4, "QEWRTYUIOPLKJHGFDSAZXCVBNM") + faker.Random.Number(0, 9),
                });
                await _Context.SaveChangesAsync();
            }

        }

        public async Task SeeddingThuocTinh()
        {
            int soluong = 20;
            var ListId = ListGuid(soluong);
            for (int i = 0; i < soluong; i++)
            {
                _Context.ThuocTinh.Add(new Entities.ThuocTinh()
                {
                    Id = ListId[i],
                    Ten = faker.Name.FirstName(),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                });
                await _Context.SaveChangesAsync();
            }

        }
        public async Task SeeddingGiaTri()
        {
            int soluong = 90;
            var ListId = ListGuid(soluong);
            var IdThuocTinh = _Context.ThuocTinh.Select(x => x.Id).ToList();
            for (int i = 0; i < soluong; i++)
            {
                _Context.GiaTri.Add(new Entities.GiaTri()
                {
                    Id = ListId[i],
                    Ten = faker.Random.Number(35, 45).ToString(),
                    TrangThai = true,
                    IdThuocTinh = faker.PickRandom(IdThuocTinh)
                });
                await _Context.SaveChangesAsync();
            }

        }

        public async Task SeeddingThuocTinhSanPham()
        {
            int soluong = 100;
            var ListId = ListGuid(soluong);
            var IdThuocTinh = _Context.ThuocTinh.Select(x => x.Id).ToList();
            var IdSanPham = _Context.SanPham.Select(x => x.Id).ToList();
            for (int i = 0; i < soluong; i++)
            {
                _Context.ThuocTinhSanPham.Add(new Entities.ThuocTinhSanPham()
                {
                    Id = ListId[i],
                    IdThuocTinh = faker.PickRandom(IdThuocTinh),
                    IdSanPham = faker.PickRandom(IdSanPham)
                });
                await _Context.SaveChangesAsync();
            }

        }

        public async Task SeeddingBienTheChiTiet()
        {
            int soluong = 100;
            var ListId = ListGuid(soluong);
            var IdGiaTri = _Context.GiaTri.Select(x => x.Id).ToList();
            var IdBienThe = _Context.BienThe.Select(x => x.Id).ToList();
            var IdThuocTinhSanPham = _Context.ThuocTinhSanPham.Select(x => x.Id).ToList();
            for (int i = 0; i < soluong; i++)
            {
                _Context.BienTheChiTiet.Add(new Entities.BienTheChiTiet()
                {
                    Id = ListId[i],
                    IdGiaTri = faker.PickRandom(IdGiaTri),
                    IdBienThe = faker.PickRandom(IdBienThe),
                    IdThuocTinhSanPham = faker.PickRandom(IdThuocTinhSanPham),
                });
                await _Context.SaveChangesAsync();
            }

        }


        public async Task SeeddingHoaDon()
        {
            //_Context.RemoveRange(_Context.HoaDon);
            // _Context.RemoveRange(_Context.ThuocTinh);
            // _Context.RemoveRange(_Context.ThuocTinhSanPham);
            //_Context.RemoveRange(_Context.HoaDonChiTiet);
            // _Context.RemoveRange(_Context.BienThe);
            // _Context.RemoveRange(_Context.BienTheChiTiet);
            // _Context.RemoveRange(_Context.NhanVien);
            // _Context.RemoveRange(_Context.KhachHang);
            //_Context.RemoveRange(_Context.SanPham);
            //await _Context.SaveChangesAsync();
            //int soluong = 300;
            //var ListId = ListGuid(soluong);
            //var ListIdChiTiet = ListGuid(soluong * 10);
            //var IdKhachHang = _Context.KhachHang.Select(x => x.Id).ToList();
            //var IdNhanVien = _Context.NhanVien.Select(x => x.Id).ToList();
            //var IdBienThe = _Context.BienThe.Select(x => x.Id).ToList();
            //var startYear = new DateTime(2000, 1, 1);
            //var endYear = DateTime.Now;
            //var range = (endYear - startYear).Days;
            //for (int i = 0; i < soluong; i++)
            //{
            //    float TienShip = faker.Random.Float(min: 5000f, max: 15000f);


            //    _Context.HoaDon.Add(new Entities.HoaDon()
            //    {
            //        Id = ListId[i],
            //        IdKhachHang = faker.PickRandom(IdKhachHang),
            //        IdNhanVien = faker.PickRandom(IdNhanVien),
            //        Ma = (i + 1).ToString().PadLeft(4, '0'),
            //        Sdt = faker.Random.Number(0, 980000000).ToString().PadLeft(11, '0'),
            //        DiaChi = faker.Address.FullAddress(),
            //        NgayNhan = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
            //        NgayGiaoHang = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
            //        NgayThanhToan = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
            //        GhiChu = faker.Lorem.Sentence(),
            //        TienShip = TienShip,
            //        TongTien = faker.Random.Float(min: 10000f, max: 5000000f) + TienShip,
            //        TrangThai = faker.Random.Int(min: 0, max: 1),
            //        LoaiDonHang = faker.Random.Int(min: 0, max: 1),
            //        TenNguoiNhan = faker.Name.FirstName(),
            //        NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),

            //    });
            //    for (int j = 0; j < faker.Random.Number(5, 10); j++)
            //    {
            //        _Context.HoaDonChiTiet.Add(new Entities.HoaDonChiTiet()
            //        {
            //            Id = ListIdChiTiet[10 * i + j],
            //            IdBienThe = faker.PickRandom(IdBienThe),
            //            SoLuong = faker.Random.Number(5, 10),
            //            IdHoaDon = ListId[i],
            //            DonGia = faker.Random.Float(min: 2000f, max: 1000f),
            //            TrangThai = 0
            //        });

            //    }

            //}
            //await _Context.SaveChangesAsync();
        }

        public async Task SeeddingKhachHang()
        {
            int soluong = 30;
            var ListId = ListGuid(soluong);
            var IdVidiem = ListGuid(soluong);
            var startYear = new DateTime(2000, 1, 1);
            var endYear = DateTime.Now;
            var range = (endYear - startYear).Days;
            for (int i = 0; i < soluong; i++)
            {
                _Context.KhachHang.Add(new Entities.KhachHang()
                {
                    Id = ListId[i],
                    // IdViDiem = IdVidiem[i],
                    Ma = (i + 1).ToString().PadLeft(2, '0'),
                    Ten = faker.Name.FirstName(),
                    NgaySinh = startYear.AddDays(faker.Random.Int(0, range)),
                    Sdt = faker.Random.Number(0, 100000).ToString().PadLeft(11, '0'),
                    DiaChi = faker.Address.FullAddress(),
                    GioiTinh = faker.Random.Bool(),
                    Email = faker.Internet.Email(),
                    Anh = faker.Image.LoremFlickrUrl(),
                    MatKhau = faker.Internet.Password(5),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                });
            }
            await _Context.SaveChangesAsync();
        }
    }
}
