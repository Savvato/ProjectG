namespace ProjectG.ClientService.Web.Controllers
{
    #region

    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.ClientService.Web.Models;

    #endregion

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}