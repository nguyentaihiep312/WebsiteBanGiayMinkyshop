using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;
using System.Text;
using Firebase.Auth;
using Firebase.Storage;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public partial class EmployeComponent
    {
        [Inject]
        private HttpClient HttpClient { get; set; } = null!;
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;
        [Inject]
        private SweetAlertService Swal { get; set; } = null!;

        [Inject]
        private ILocalStorageService local { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Parameter]
        public int Page { get; set; }
        private PaginationRequest PaginationRequest = new PaginationRequest() { PerPage = 5 };
        private Response<PaginationResponse<NhanVien>> Response = new Response<PaginationResponse<NhanVien>>();
        private string url = "https://localhost:7053/api/NhanViens";
        private string query = "";
        private bool viewform = false;
        private bool Create = true;
        private NhanVienModel.NhanVienFormModel Model = new NhanVienModel.NhanVienFormModel();
        private Guid IdNhanVien = Guid.Empty;
        private int maxAllowFiles = int.MaxValue;
        private long maxSizeFile = long.MaxValue;
        private static string ApiKey = "AIzaSyC1CQ9feCUbui7LV6qId8nesbF9TNma05E";
        private static string Bucket = "imagesangularapp.appspot.com";
        private static string AuthEmail = "truong@gmail.com";
        private static string AuthPassword = "123456";
        private Guid IdNhanVienDangNhap = Guid.Empty;
        private List<NhanVien> lstNhanVien = new();

        protected override async Task OnParametersSetAsync()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(await local.GetItemAsStringAsync("Token"));
            var Role = jwt.Claims.FirstOrDefault(c => c.Type.Equals("VaiTro"))?.Value;
            var Id = jwt.Claims.FirstOrDefault(c => c.Type.Equals("Id"))?.Value;
            Model.Ma = $"NV{Helper.RandomNumber(5)}";
            IdNhanVienDangNhap = Guid.Parse(Id);
            if (Role == "1")
            {
                Navigation.NavigateTo("/admin");
                await Swal.FireAsync("Thông báo", "Bạn Không Có Quyền Truy Cập Vào Quản Lí Nhân Viên", SweetAlertIcon.Warning);
            }

            PaginationRequest.CurrentPage = Page;
            await Get();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice");
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice2");
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice-size");
            }
        }

        async Task Get()
        {
            query = $"?PerPage={PaginationRequest.PerPage}&CurrentPage={PaginationRequest.CurrentPage}&Status={PaginationRequest.Status}&Role={PaginationRequest.Role}&Keyword={PaginationRequest.Keyword}";

            Response = await HttpClient.GetFromJsonAsync<Response<PaginationResponse<NhanVien>>>($"{url}{query}");

            lstNhanVien = Response.Data.Data.Where(c => c.Id != IdNhanVienDangNhap).ToList();
        }

        async Task FilterWithStatus(ChangeEventArgs e)
        {
            int? status = e.Value.ToString() == "" ? null : Convert.ToInt32(e.Value.ToString());
            PaginationRequest.Status = status;
            await Get();
        }

        async Task FilterWithRole(ChangeEventArgs e)
        {
            int? role = e.Value.ToString() == "" ? null : Convert.ToInt32(e.Value.ToString());
            PaginationRequest.Role = role;
            await Get();
        }

        async Task ChangePerPage(ChangeEventArgs e)
        {
            int perPage = Convert.ToInt32(e.Value.ToString());
            PaginationRequest.PerPage = perPage;
            await Get();
        }

        async Task Search(ChangeEventArgs e)
        {
            string? keyword = e.Value.ToString() == "" ? null : e.Value.ToString();
            PaginationRequest.Keyword = keyword;
            await Get();
        }

        async Task Remove(Guid Id)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Xóa Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            string idNhanVien = $"/{Id}";

            var result = await HttpClient.DeleteAsync($"{url}{idNhanVien}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            if (result.IsSuccessStatusCode)
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Success
                });
                await Get();
            }
            else
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Text = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Error
                });
            }
        }

        async Task ViewForm()
        {
            viewform = true;
            Model.Ma = $"NV{Helper.RandomNumber(5)}";
        }

        async Task CancelForm()
        {
            viewform = false;
            await ResetModel();
            if (!Create)
            {
                Create = true;
            }
        }

        async Task ResetModel()
        {
            Model = new NhanVienModel.NhanVienFormModel();
        }

        async Task Submit(EditContext editContext)
        {
            if (editContext.Validate())
            {
                if (Create)
                {
                    if (ErrorAnh != string.Empty ||
                        ErrorName != string.Empty ||
                        ErrorPass != string.Empty ||
                        ErrorEmail != string.Empty ||
                        ErrorDate != string.Empty ||
                        ErrorSdt != string.Empty)
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Text = "Vui lòng kiểm tra lại thông tin",
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Warning
                        });
                    }
                    else if (string.IsNullOrEmpty(Model.DiaChi) 
                        || string.IsNullOrEmpty(Model.Email) 
                        || string.IsNullOrEmpty(Model.MatKhau)
                        || string.IsNullOrEmpty(Model.Sdt)
                        || string.IsNullOrEmpty(Model.Ten))
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Text = "Vui lòng kiểm tra lại thông tin",
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Warning
                        });
                    }
                    else
                    {
                        var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Thêm Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });

                        if (string.IsNullOrEmpty(confirm.Value)) return;

                        Model.TrangThai = 1;
                        var result = await HttpClient.PostAsJsonAsync(url, Model);
                        var response = await result.Content.ReadFromJsonAsync<Response>();

                        if (result.IsSuccessStatusCode)
                        {
                            await Swal.FireAsync(new SweetAlertOptions
                            {
                                Text = response.Message,
                                ShowConfirmButton = true,
                                Icon = SweetAlertIcon.Success
                            });
                            await Get();
                            await ResetModel();
                            viewform = false;
                        }
                        else
                        {
                            await Swal.FireAsync(new SweetAlertOptions
                            {
                                Text = response.Message,
                                ShowConfirmButton = true,
                                Icon = SweetAlertIcon.Error
                            });
                        }
                    }
                }
                else
                {
                    if (ErrorAnh != string.Empty ||
                       ErrorName != string.Empty ||
                       ErrorPass != string.Empty ||
                       ErrorEmail != string.Empty ||
                       ErrorDate != string.Empty ||
                       ErrorSdt != string.Empty)
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Text = "Vui lòng kiểm tra lại thông tin",
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Warning
                        });
                    }
                    else if (string.IsNullOrEmpty(Model.DiaChi)
                        || string.IsNullOrEmpty(Model.Email)
                        || string.IsNullOrEmpty(Model.MatKhau)
                        || string.IsNullOrEmpty(Model.Sdt)
                        || string.IsNullOrEmpty(Model.Ten))
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Text = "Vui lòng kiểm tra lại thông tin",
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Warning
                        });
                    }
                    else
                    {
                        var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });
                        if (string.IsNullOrEmpty(confirm.Value)) return;

                        var result = await HttpClient.PutAsJsonAsync($"{url}/{IdNhanVien}", Model);
                        var response = await result.Content.ReadFromJsonAsync<Response>();

                        if (result.IsSuccessStatusCode)
                        {
                            await Swal.FireAsync(new SweetAlertOptions
                            {
                                Title = response.Message,
                                ShowConfirmButton = true,
                                Icon = SweetAlertIcon.Success
                            });
                            await Get();
                            await ResetModel();
                            viewform = false;
                            Create = true;
                        }
                        else
                        {
                            await Swal.FireAsync(new SweetAlertOptions
                            {
                                Text = response.Message,
                                ShowConfirmButton = true,
                                Icon = SweetAlertIcon.Error
                            });
                        }
                    }
                }
            }
        }

        async Task GetNhanVien(Guid Id)
        {
            var response = await HttpClient.GetFromJsonAsync<Response<NhanVien>>($"{url}/{Id}");
            var nhanvien = response.Data;
            Model = new NhanVienModel.NhanVienFormModel()
            {
                Anh = nhanvien.Anh,
                DiaChi = nhanvien.DiaChi,
                Email = nhanvien.Email,
                GioiTinh = nhanvien.GioiTinh,
                Ma = nhanvien.Ma,
                MatKhau = nhanvien.MatKhau,
                NgaySinh = nhanvien.NgaySinh,
                Sdt = nhanvien.Sdt,
                Ten = nhanvien.Ten,
                TrangThai = nhanvien.TrangThai,
                VaiTro = nhanvien.VaiTro,
            };
            viewform = true;
            Create = false;
            IdNhanVien = nhanvien.Id;
        }

        private string ErrorAnh = string.Empty;
        async Task UploadImage(InputFileChangeEventArgs e)
        {
            try
            {
                var file = e.File;
                var stream = file.OpenReadStream(maxSizeFile);
                var extension = Path.GetExtension(file.Name);

                Console.WriteLine(extension);
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ErrorAnh = "Vui lòng chọn file ảnh";
                    Model.Anh = null;
                }
                else
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                    var cancellation = new CancellationTokenSource();

                    var task = new FirebaseStorage(
                        Bucket,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true
                        })
                        .Child("images")
                        .Child(file.Name)
                        .PutAsync(stream, cancellation.Token);
                    Model.Anh = await task;
                    ErrorAnh = string.Empty;
                }
            }
            catch (Exception ex)
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Text = ex.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Error
                });
            }
        }

        async Task ChangeStatus(Guid Id, int Status)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa Trạng Thái", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });
            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{url}/ChangeStatus/{Id}/{Status}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            if (result.IsSuccessStatusCode)
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Text = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Success
                });
                await Get();
            }
            else
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Text = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Error
                });
                await Get();
            }
        }

        private string ErrorName = string.Empty;
        private async Task ValidateTen(ChangeEventArgs e)
        {
            var Regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");

            var value = e.Value.ToString();
            if (string.IsNullOrEmpty(value))
            {
                ErrorName = "Vui lòng nhập tên nhân viên";
            }
            else if (Regex.IsMatch(value))
            {
                ErrorName = "Vui lòng không nhập số";
            }
            else if (value.Length < 10)
            {
                ErrorName = "Tên nhân viên phải lớn hơn 10 ký tự";
            }
            else
            {
                ErrorName = string.Empty;
            }
        }

        private string ErrorSdt = string.Empty;
        private async Task ValidateSdt(ChangeEventArgs e)
        {
            var Regex = new Regex(@"^[-+]?[0-9]*.?[0-9]+$");

            var value = e.Value.ToString();
            if (string.IsNullOrEmpty(value))
            {
                ErrorSdt = "Vui lòng nhập số điện thoại";
            }
            else if (!Regex.IsMatch(value))
            {
                ErrorSdt = "Số điện thoại phải là số";
            }
            else if (int.Parse(value) < 0)
            {
                ErrorSdt = "Vui lòng không nhập giá trị âm";
            }
            else if (value.Length < 10)
            {
                ErrorSdt = "Số điện thoại không được nhỏ hơn 10 ký tự";
            }
            else
            {
                ErrorSdt = string.Empty;
            }
        }

        private string ErrorEmail = string.Empty;
        private async Task ValidateEmail(ChangeEventArgs e)
        {
            var Regex = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            var value = e.Value.ToString();
            if (string.IsNullOrEmpty(value))
            {
                ErrorEmail = "Vui lòng nhập Email";
            }
            else if (!Regex.IsMatch(value))
            {
                ErrorEmail = "Email sai định dạng";
            }
            else
            {
                ErrorEmail = string.Empty;
            }
        }

        private string ErrorPass = string.Empty;
        private async Task ValidatePass(ChangeEventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            var value = e.Value.ToString();
            if (string.IsNullOrEmpty(value))
            {
                ErrorPass = "Vui lòng nhập mật khẩu";
            }
            else if (!hasNumber.IsMatch(value))
            {
                ErrorPass = "Mật khẩu phải có ký tự là số";
            }
            else if (!hasUpperChar.IsMatch(value))
            {
                ErrorPass = "Mật khẩu phải có ký tự là chữ in hoa";
            }
            else if (!hasLowerChar.IsMatch(value))
            {
                ErrorPass = "Mật khẩu phải có ký tự là chữ thường";
            }
            else if (!hasSymbols.IsMatch(value))
            {
                ErrorPass = "Mật khẩu phải có ký tự là ký tự đặc biệt";
            }
            else if (value.Length < 6 || value.Length > 20)
            {
                ErrorPass = "Mật khẩu phải lớn hơn 6 và nhỏ hơn 20 ký tự";
            }
            else
            {
                ErrorPass = string.Empty;
            }
        }

        private string ErrorDate = string.Empty;
        private async Task ValidateDate(ChangeEventArgs e)
        {
            var value = e.Value.ToString();
            var year = int.Parse(value.Split("-")[0]);
            var age = DateTime.Now.Year - int.Parse(value.Split("-")[0]);
            if (year >= DateTime.Now.Year)
            {
                ErrorDate = "Vui lòng chọn năm sinh nhỏ hơn năm hiện tại";
            }
            else if (age < 18)
            {
                ErrorDate = "Nhân viên phải đủ 18 tuổi";
            }
            else
            {
                ErrorDate = string.Empty;
                Model.NgaySinh = DateTime.Parse(value);
            }
        }
    }
}
