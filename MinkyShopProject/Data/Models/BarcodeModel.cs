using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class BarcodeModel
    {
        public string Product { get; set; }

        public List<BarcodeModelString> Barcodes { get; set; } = new List<BarcodeModelString>();
    }

    public class BarcodeModelString
    {
        public string Barcode = null!;

        public string BarcodeText = null!;
    }
}
