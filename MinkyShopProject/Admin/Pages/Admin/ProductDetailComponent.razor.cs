using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using MinkyShopProject.Admin.Shared;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public partial class ProductDetailComponent
    {
        int maxAllowedFiles = int.MaxValue;
        long maxFileSize = long.MaxValue;
        List<string> fileNames = new List<string>();
        List<MinkyShopProject.Data.Models.UploadResult> uploadResults = new List<MinkyShopProject.Data.Models.UploadResult>();

        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        private int SoLuongChung = 0;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        public int IdBienThe = 999;

        [Parameter]
        public Guid IdSanPham { get; set; }

        bool showAll = false;

        ResponsePagination<NhomSanPhamModel>? NhomSanPhamModels;

        List<NhomSanPhamModel>? NhomSanPhamModelsParent;

        ResponseObject<SanPhamModel>? SanPham;

        List<string>? ModelImage;

        bool showNhomSanPham = false;

        string Url = "https://localhost:7053/api";

        private InputFile? inputFile;

        private ElementReference previewImageElem;

        float GiaChung = 0;

        private async Task ShowPreview() => await JSRuntime.InvokeVoidAsync(
        "previewImage", inputFile!.Element, previewImageElem);

        void ApDungGiaChung()
        {
            if (SanPham?.Data.BienThes != null)
            {
                foreach (var x in SanPham.Data.BienThes)
                {
                    x.GiaBan = GiaChung;
                }
                GiaChung = 0;
            }
        }

        void ApDungSoLuongChung()
        {
            if (SanPham?.Data.BienThes != null)
            {
                foreach (var x in SanPham.Data.BienThes)
                {
                    x.SoLuong = SoLuongChung;
                }
                SoLuongChung = 0;
            }
        }

        void ApDungAnh()
        {
            foreach (var x in SanPham.Data.BienThes)
            {
                x.Anh = SanPham.Data.Anh;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<ResponsePagination<NhomSanPhamModel>>($"{Url}/NhomSanPham");
            if (NhomSanPhamModels != null && NhomSanPhamModels.Data.Content.Any())
            {
                NhomSanPhamModelsParent = NhomSanPhamModels.Data.Content;
            }
            try
            {
                var result = await HttpClient.GetFromJsonAsync<List<string>>("https://localhost:7053/api/Image");
                if (result != null)
                {
                    ModelImage = result;
                }
            }
            catch (Exception)
            {
                ModelImage = new List<string>();
            }
        }

        async Task DeleteAsync(Guid id)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Xóa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{Url}/BienThe?id={id}");

            if (result.IsSuccessStatusCode)
                SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
        }

        async Task UpdateSanPhamAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Sửa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            if (SanPham?.Data.BienThes != null)
            {
                foreach (var x in SanPham.Data.BienThes)
                {
                    x.BienTheChiTiets = null;
                }
            }

            var result = await HttpClient.PutAsJsonAsync($"{Url}/SanPham/{SanPham?.Data.Id}", SanPham?.Data);

            if (result.IsSuccessStatusCode)
                await Swal.FireAsync("Thông Báo", "Cập Nhật Thành Công", SweetAlertIcon.Success);
        }

        private async void LoadFiles(InputFileChangeEventArgs e)
        {
            using var content = new MultipartFormDataContent();

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                fileNames.Add(file.Name);
                content.Add(content: fileContent, name: "\"files\"", fileName: file.Name);
            }

            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Muốn Tải Ảnh Lên Không", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var responose = await HttpClient.PostAsync(Url + "/image", content);

            if (responose.IsSuccessStatusCode)
            {
                await Swal.FireAsync("", "Thêm thành công", SweetAlertIcon.Success);
                ModelImage = await HttpClient.GetFromJsonAsync<List<string>>("https://localhost:7053/api/Image");
            }
            else
            {
                await Swal.FireAsync("", "Không đúng định dạng", SweetAlertIcon.Error);
            }
        }
    }
}
