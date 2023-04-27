using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public static class LoginModels
    {
        public class Login
        {
            public string? SoDienThoaiOrEmail { get; set; }

            public string? MatKhau { get; set; }
        }

        public class LoginResultModel
        {
            public string? Token { get; set; }

            public string? Message { get; set; }
        }
    }
}
