using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Common;
using CurrieTechnologies.Razor.SweetAlert2;
using MinkyShopProject.Data.Enums;
using System.IdentityModel.Tokens.Jwt;
using MinkyShopProject.Business.Entities;
using System.Net.NetworkInformation;
using Blazored.SessionStorage;

namespace MinkyShopProject.Admin.Pages.Sale
{
    public partial class SaleComponent
    {
        [Inject]
        ILocalStorageService local { get; set; } = null!;

        Guid Id = Guid.Empty;

        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        ILocalStorageService Session { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        List<HoaDonCreateModel> HoaDons = new List<HoaDonCreateModel>();

        ResponsePagination<VoucherModel>? Vouchers;

        List<VoucherModel>? VouchersThoaMan;

        ResponsePagination<KhachHangModel>? KhachHangs;

        ResponsePagination<SanPhamModel>? SanPhams;

        List<SanPhamModel> SanPhamsSearch = new List<SanPhamModel>();

        List<KhachHangModel> KhachHangsSeach = new List<KhachHangModel>();

        Uri Url = new Uri("https://localhost:7053/api/");

        int index = 0;

        float soTienThanhToan = 0;

        bool ShowScanBarcode = false;

        string ma = "";

        List<HinhThucThanhToanModel>? HinhThucThanhToans;

        private GiaoCa Ca = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        private string Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        private async Task ScanResult(string e)
        {
            await ThemTuMaSanPham(e);
        }

        async Task LuuDuLieu()
        {
            await Session.SetItemAsync("invoice", HoaDons);
            await Swal.FireAsync("", "Lưu thành công", SweetAlertIcon.Success);
        }

        protected override async Task OnInitializedAsync()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(await local.GetItemAsStringAsync("Token"));
            var IdNhanVien = jwt.Claims.FirstOrDefault(c => c.Type.Equals("Id"))?.Value;
            var result = await HttpClient.GetFromJsonAsync<Response<GiaoCa>>($"https://localhost:7053/api/GiaoCas?Id={Guid.Parse(IdNhanVien)}&ThoiGian={DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}");
            Ca = result.Data;
            if (Ca == null)
            {
                await Swal.FireAsync("Thông Báo", "Vui Lòng Bắt Đầu Ca Làm Việc", SweetAlertIcon.Warning);
                Navigation.NavigateTo("/admin");
            }
            SanPhams = await HttpClient.GetFromJsonAsync<ResponsePagination<SanPhamModel>>(Url + "sanpham");
            KhachHangs = await HttpClient.GetFromJsonAsync<ResponsePagination<KhachHangModel>>(Url + "khachhang/get");
            Vouchers = await HttpClient.GetFromJsonAsync<ResponsePagination<VoucherModel>>(Url + "voucher");
            await Reload();
        }

        async Task TimKiemKhachHang(string val)
        {
            if (KhachHangs != null)
                KhachHangsSeach = KhachHangs.Data.Content.Where(c => c.Ten.ToLower().Trim().Contains(val.ToLower().Trim())).ToList();
        }

        async Task CapNhatTongTien()
        {
            if (HoaDons != null && HoaDons.Any())
            {
                var hoaDon = HoaDons[index];
                hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.DonGia * c.SoLuong);
            }
        }

        async Task ThemTuMaSanPham(string value)
        {
            if (SanPhams != null)
            {
                var tonTai = false;
                foreach (var x in SanPhams.Data.Content)
                {
                    if (x.BienThes != null)
                    {
                        foreach (var y in x.BienThes)
                        {
                            if (y.Sku.ToLower().Trim() == value.ToLower().Trim())
                            {
                                y.SoLuongTam = 1;
                                await ThemSanPham(new List<BienTheModel>() { y });
                                tonTai = true;
                            }
                        }
                    }
                }
            }
        }

        async Task TimKiemSanPham(string value)
        {
            if (SanPhams != null)
            {
                SanPhamsSearch = SanPhams.Data.Content.Where(c => c.Ten.ToLower().Trim().Contains(value.ToLower().Trim())).ToList();
            }
        }

        async Task XoaSanPham(int hdct)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                HoaDons[index].HoaDonChiTiets.RemoveAt(hdct);
                await CapNhatTongTien();
                await TimKiemVoucher();
            }
        }

        public async Task ThemSanPham(List<BienTheModel> obj)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                foreach (var x in obj)
                {
                    Console.WriteLine($"{x.SoLuongTam} {x.SoLuong}");
                    if (x.SoLuongTam > x.SoLuong)
                    {
                        await Swal.FireAsync("", $"{x.SanPham?.Ten} Không Đủ Sản Phẩm Trong Kho", SweetAlertIcon.Error);
                        return;
                    }

                    var hdct = HoaDons[index].HoaDonChiTiets.FirstOrDefault(c => c.IdBienThe == x.Id);
                    if (hdct != null && hdct.BienThe != null)
                    {
                        hdct.SoLuong += x.SoLuongTam;
                        hdct.SoLuongGoc = x.SoLuong;
                    }
                    else
                    {
                        HoaDons[index].HoaDonChiTiets.Add(new HoaDonChiTietModel() { IdBienThe = x.Id, SoLuong = x.SoLuongTam, SoLuongGoc = x.SoLuong, DonGia = x.GiaBan, BienThe = x });
                    }
                }
                HoaDons[index].TongTien = HoaDons[index].HoaDonChiTiets.Sum(c => c.DonGia * c.SoLuong);
                await TimKiemVoucher();
                StateHasChanged();
            }
        }

        void XoaHinhThucThanhToan(int kieuThanhToan)
        {
            if (HinhThucThanhToans != null)
            {
                var hinhThucThanhToan = HinhThucThanhToans.FirstOrDefault(c => c.KieuThanhToan == kieuThanhToan);
                if (hinhThucThanhToan != null)
                {
                    HinhThucThanhToans.Remove(hinhThucThanhToan);
                    if (HinhThucThanhToans.Count <= 0)
                    {
                        HinhThucThanhToans.Add(new HinhThucThanhToanModel() { KieuThanhToan = 0, TongTienThanhToan = 0 });
                    }
                }
            }
        }

        public async Task ThemHinhThucThanhToan(int kieuThanhToan)
        {
            if (HinhThucThanhToans != null)
            {
                var hinhThucThanhToan = HinhThucThanhToans.FirstOrDefault(c => c.KieuThanhToan == kieuThanhToan);
                if (hinhThucThanhToan == null)
                {
                    HinhThucThanhToans.Add(new HinhThucThanhToanModel() { KieuThanhToan = kieuThanhToan, TongTienThanhToan = soTienThanhToan });
                }
                else
                {
                    hinhThucThanhToan.TongTienThanhToan += soTienThanhToan;
                }
                soTienThanhToan = 0;
            }
        }

        async Task ThemHoaDon()
        {
            if (HoaDons != null && HoaDons.Any() && HoaDons.Count < 10)
            {
                HoaDons.Add(new HoaDonCreateModel());
                await JSRuntime.InvokeVoidAsync("overflow");
            }
            else
            {
                await Swal.FireAsync("", "Không Thể Thêm Quá 10 Hóa Đơn", SweetAlertIcon.Error);
            }
        }

        async Task XoaHoaDon(int index)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                HoaDons.RemoveAt(index);
                await JSRuntime.InvokeVoidAsync("overflow");
                if (HoaDons.Count <= 0)
                {
                    HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() { } };
                }
            }
        }

        async Task Reload()
        {
            //if (Session.GetItemAsStringAsync("invoice") != null)
            //{
            //    HoaDons = await Session.GetItemAsync<List<HoaDonCreateModel>>("invoice");
            //}
            //else
            //{
            //   
            //}
            HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() };
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(await local.GetItemAsStringAsync("Token"));
            var IdNhanVien = jwt.Claims.FirstOrDefault(c => c.Type.Equals("Id"))?.Value;
            if (IdNhanVien != null)
            {
                var result2 = await HttpClient.GetFromJsonAsync<Response<NhanVienModel.NhanVienCreateModel>>($"https://localhost:7053/api/NhanViens/{IdNhanVien}");
                if (result2 != null)
                {
                    HoaDons[index].IdNhanVien = Guid.Parse(IdNhanVien);
                    HoaDons[index].NhanVien = result2.Data;
                }
            }
        }

        async Task<bool> InHoaDon()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn In Hóa Đơn", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return false;
            return true;
        }

        async Task ThemHoaDonVaoCsdl()
        {
            var hoaDon = HoaDons[index];

            if (hoaDon != null)
            {
                if (hoaDon.LoaiDonHang == (int)LoaiHoaDon.DatHang || hoaDon.LoaiDonHang == (int)LoaiHoaDon.GiaoHang)
                {
                    var validate = await ValidateDatHang();

                    if (validate)
                    {

                        var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Thêm Hóa Đơn", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

                        if (string.IsNullOrEmpty(confirm.Value)) return;

                        hoaDon.TrangThai = TrangThaiHoaDon.ChoXacNhan;

                        foreach (var x in hoaDon.HoaDonChiTiets)
                        {
                            x.BienThe = null;
                        }

                        if (hoaDon.VoucherLogs != null && hoaDon.VoucherLogs.Any())
                        {
                            hoaDon.VoucherLogs[0].Voucher = null;
                        }

                        HoaDons[index].IdNhanVien = Ca.IdNhanVienTrongCa;

                        var status = await HttpClient.PostAsJsonAsync(Url + "hoadon", HoaDons[index]);
                        if (status.IsSuccessStatusCode)
                        {
                            HoaDons.Remove(HoaDons[index]);
                            await Swal.FireAsync("", "Thêm Thành Công", SweetAlertIcon.Success);
                        }
                        else
                        {
                            await Swal.FireAsync("", "Thêm Thất Bại", SweetAlertIcon.Error);
                        }
                        if (HoaDons.Count() <= 0 && !HoaDons.Any())
                        {
                            HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() };
                        }
                    }
                }
                else
                {
                    var validate = await ValidateHoaDon();
                    if (validate)
                    {
                        var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Thêm Hóa Đơn", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

                        if (string.IsNullOrEmpty(confirm.Value)) return;

                        if (HoaDons != null && HoaDons.Any())
                        {
                            if (hoaDon != null)
                            {
                                hoaDon.NgayCapNhat = DateTime.Now;
                                hoaDon.NgayHoanThanh = DateTime.Now;
                                hoaDon.Id = Guid.NewGuid();

                                foreach (var x in hoaDon.HoaDonChiTiets)
                                {
                                    x.BienThe = null;
                                }

                                if (hoaDon.VoucherLogs != null && hoaDon.VoucherLogs.Any())
                                {
                                    hoaDon.VoucherLogs[0].Voucher = null;
                                }
                            }

                            //HoaDons[index].IdNhanVien = Ca.IdNhanVienTrongCa;
                            HoaDons[index].IdNhanVien = Ca.IdNhanVienTrongCa;

                            var status = await HttpClient.PostAsJsonAsync(Url + "hoadon", HoaDons[index]);
                            if (status.IsSuccessStatusCode)
                            {
                                HoaDons.Remove(HoaDons[index]);
                                await Swal.FireAsync("", "Thêm Thành Công", SweetAlertIcon.Success);
                                if (await InHoaDon())
                                {
                                    Navigation.NavigateTo("print/" + hoaDon.Id);
                                }
                            }
                            else
                            {
                                await Swal.FireAsync("", "Thêm Thất Bại", SweetAlertIcon.Error);
                            }
                            if (HoaDons.Count() <= 0 && !HoaDons.Any())
                            {
                                HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() };
                            }
                        }
                    }
                }
            }
        }

        async Task<bool> ValidateHoaDon()
        {
            if (HoaDons != null && HoaDons.Any())
            {
                var hoaDon = HoaDons[index];
                if (!hoaDon.HoaDonChiTiets.Any())
                {
                    await Swal.FireAsync("", "Hóa Đơn Chưa Có Sản Phẩm", SweetAlertIcon.Error);
                    return false;
                }
                else if (hoaDon != null)
                {
                    var voucher = hoaDon.VoucherLogs != null && hoaDon.VoucherLogs.Any();
                    if (!voucher)
                    {
                        if (hoaDon.HinhThucThanhToans.Sum(c => c.TongTienThanhToan) + hoaDon.TienShip < HoaDons[index].TongTien)
                        {
                            await Swal.FireAsync("", "Hóa Đơn Chưa Chưa Trả Đủ Tiền", SweetAlertIcon.Error);
                            return false;
                        }
                    }
                    else
                    {
                        if (hoaDon.HinhThucThanhToans.Sum(c => c.TongTienThanhToan) < hoaDon.VoucherLogs?[0].TienSauKhiGiam)
                        {
                            await Swal.FireAsync("", "Hóa Đơn Chưa Chưa Trả Đủ Tiền", SweetAlertIcon.Error);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        async Task<bool> ValidateDatHang()
        {
            if (HoaDons != null && HoaDons.Any())
            {
                var hoaDon = HoaDons[index];
                if (!hoaDon.HoaDonChiTiets.Any())
                {
                    await Swal.FireAsync("", "Hóa Đơn Chưa Có Sản Phẩm", SweetAlertIcon.Error);
                    return false;
                }
                else if (hoaDon != null)
                {
                    if (string.IsNullOrEmpty(hoaDon.DiaChi?.Trim()) || string.IsNullOrEmpty(hoaDon.TenNguoiNhan?.Trim()) || string.IsNullOrEmpty(hoaDon.Sdt))
                    {
                        await Swal.FireAsync("", "Vui Lòng Điền Đầy Đủ Thông Tin", SweetAlertIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        async Task TimKiemVoucher()
        {
            VouchersThoaMan = Vouchers?.Data.Content.Where(c => c.SoTienCan <= HoaDons[index].TongTien && c.SoLuong > 0 && c.NgayKetThuc >= DateTime.Now).ToList();
        }

        async Task ApDungVoucher(int indexVoucher)
        {
            var voucher = VouchersThoaMan?[indexVoucher];

            if (voucher != null)
            {
                if (HoaDons[index] != null && HoaDons.Any())
                {
                    if (voucher.HinhThucGiamGia == 1)
                    {
                        HoaDons[index].VoucherLogs = new List<VoucherLogModel>() { new VoucherLogModel() { IdVoucher = voucher.Id, SoTienGiam = voucher.SoTienGiam, TienTruocKhiGiam = HoaDons[index].TongTien, TienSauKhiGiam = HoaDons[index].TongTien - voucher.SoTienGiam, Voucher = voucher } };
                        await CapNhatTongTien();
                    }
                    else
                    {
                        var after = HoaDons[index].TongTien - (HoaDons[index].TongTien * voucher.SoTienGiam / 100);
                        HoaDons[index].VoucherLogs = new List<VoucherLogModel>() { new VoucherLogModel() { IdVoucher = voucher.Id, SoTienGiam = HoaDons[index].TongTien - after, TienTruocKhiGiam = HoaDons[index].TongTien, TienSauKhiGiam = after, Voucher = voucher } };
                    }
                }
                VouchersThoaMan = null;
            }
        }
    }
}
