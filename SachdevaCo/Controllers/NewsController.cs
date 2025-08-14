using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model;


namespace SachdevaCo.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}