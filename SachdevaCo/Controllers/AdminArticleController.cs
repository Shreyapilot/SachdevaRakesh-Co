using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;


namespace SachdevaCo.Web.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class AdminArticleController : Controller
    {

        private readonly IArticleRepository _articleRepo;

        public AdminArticleController(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Content("❌ You are not authorized to access this page.");
            }
            var articles = _articleRepo.GetAllArticles() ?? new List<ArticleViewModel>();
            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _articleRepo.AddArticlesAsync(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleRepo.DeleteArticlesAsync(id);
            return RedirectToAction("Index");
        }
    }
}