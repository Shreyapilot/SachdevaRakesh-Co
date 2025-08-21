using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model;
using SachdevaCo.Core.Model.IRepository;
using System.Diagnostics;


namespace SachdevaCo.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewsRepository _newsRepo;


        public NewsController(ILogger<NewsController> logger, INewsRepository newsRepo)
        {
            _logger = logger;
            _newsRepo = newsRepo;
        }
        public IActionResult Index(string? category)
        {
            var news = _newsRepo.GetAllNews();
            if (!string.IsNullOrEmpty(category))
            {
                news = news
                    .Where(n => n.Category != null && n.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.SelectedCategory = category;
            return View(news);
        }
    }
}