using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Controllers
{
    public class FoodShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult CheckOrder()
        {
            return View();
        }
    }
}
