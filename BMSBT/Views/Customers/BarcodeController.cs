using Microsoft.AspNetCore.Mvc;

namespace BMSBT.Views.Customers
{
    public class BarcodeController : Controller
    {
        public IActionResult Index()
        {
            string rawData = "Shahid Ghauri";
            string barcodeData = $"*{rawData}*"; // Wrap with asterisks

            ViewBag.Barcode = barcodeData;
            return View();
        }
    }
}
