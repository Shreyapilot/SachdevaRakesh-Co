using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
namespace SachdevaCo.Controllers
{
 
    public class AdminNewsController : Controller
    {
        private readonly INewsRepository _newsRepo;

        public AdminNewsController(INewsRepository newsRepo)
        {
            _newsRepo = newsRepo;
        }
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Content("❌ You are not authorized to access this page.");
            }
            var news = _newsRepo.GetAllNews() ?? new List<NewsViewModel>();
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _newsRepo.AddNewsAsync(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsRepo.DeleteNewsAsync(id);
            return RedirectToAction("Index");
        }
    }
}
