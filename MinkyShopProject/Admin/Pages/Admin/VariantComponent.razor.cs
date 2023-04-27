using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public class ThuocTinh
    {
        public bool Show { get; set; } = false;

        public bool Disabled { get; set; } = false;
    }

    public partial class VariantComponent
    {
        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = null!;

        List<ThuocTinh> ThuocTinhs = new List<ThuocTinh>();

        string GiaTri = "";

        bool showNhomSanPham = false;

        string Url = "https://localhost:7053/api";

        List<bool> showGiaTri = new List<bool>();

        ResponsePagination<ThuocTinhModel>? ThuocTinhModels;

        List<ThuocTinhModel>? ThuocTinhModelsParent;

        List<ThuocTinhModel> ThuocTinhModelsTemplate = new List<ThuocTinhModel>();

        ResponsePagination<NhomSanPhamModel>? NhomSanPhamModels;

        List<NhomSanPhamModel>? NhomSanPhamModelsParent;

        SanPhamModel SanPham = new SanPhamModel();

        protected override async Task OnInitializedAsync()
        {
            ThuocTinhModels = await HttpClient.GetFromJsonAsync<ResponsePagination<ThuocTinhModel>>($"{Url}/ThuocTinh");
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<ResponsePagination<NhomSanPhamModel>>($"{Url}/NhomSanPham");
            if (NhomSanPhamModels != null && NhomSanPhamModels.Data.Content.Any())
            {
                NhomSanPhamModelsParent = NhomSanPhamModels.Data.Content;
            }
            if (ThuocTinhModels != null && ThuocTinhModels.Data.Content.Any())
            {
                ThuocTinhModelsParent = ThuocTinhModels.Data.Content;
                foreach (var x in ThuocTinhModelsParent)
                {
                    x.GiaTriParent = x.GiaTris;
                }
            }
        }

        async Task<bool> Validate()
        {
            if (ThuocTinhModelsTemplate == null || !ThuocTinhModelsTemplate.Any() || string.IsNullOrEmpty(SanPham.Ten?.ToLower().Trim()))
            {
                await Swal.FireAsync("", "Sản phẩm phải có tên và ít nhất một thuộc tính", SweetAlertIcon.Error);
                return false;
            }
            else if (ThuocTinhModelsTemplate != null && ThuocTinhModelsTemplate.Any(c => string.IsNullOrEmpty(c.Ten) || string.IsNullOrEmpty(c.Ten.Trim())))
            {
                foreach (var x in ThuocTinhModelsTemplate)
                {
                    x.Ten = "";
                }
                await Swal.FireAsync("", "Thuộc tính không thể bỏ trống", SweetAlertIcon.Error);
                return false;
            }
            else if (ThuocTinhModelsTemplate != null && ThuocTinhModelsTemplate.Any(c => c.GiaTriTemplates != null && !c.GiaTriTemplates.Any()))
            {
                await Swal.FireAsync("", "Giá trị không thể bỏ trống", SweetAlertIcon.Error);
                return false;
            }
            return true;
        }

        public async Task AddAsync()
        {
            if (await Validate())
            {
                var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Thêm", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

                if (string.IsNullOrEmpty(confirm.Value)) return;

                if (ThuocTinhModelsTemplate != null && ThuocTinhModelsTemplate.Count() > 0)
                {

                    var thuocTinhs = new List<ThuocTinhModel>();

                    foreach (var x in ThuocTinhModelsTemplate)
                    {
                        if (x.GiaTriTemplates.Count() > 0)
                        {
                            thuocTinhs.Add(new ThuocTinhModel() { Ten = x.Ten, Id = x.Id, GiaTris = x.GiaTriTemplates });
                        }
                    }

                    SanPham.Id = Guid.NewGuid();

                    var result = await HttpClient.PostAsJsonAsync($"{Url}/BienThe", new BienTheCreateModel() { ThuocTinhs = thuocTinhs, SanPham = SanPham });

                    if (result.IsSuccessStatusCode)
                    {
                        ThuocTinhModelsTemplate = new List<ThuocTinhModel>();
                        NavigationManager.NavigateTo("/admin/product/" + SanPham.Id);
                    }
                    else
                    {
                        var error = await result.Content.ReadFromJsonAsync<Response>();
                        await Swal.FireAsync("", error?.Message, SweetAlertIcon.Error);
                    }
                }
            }
        }
    }
}