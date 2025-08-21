using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminAboutController : Controller
    {
        private readonly IAboutRepository _aboutRepo;
        private readonly ITeamMemberRepository _teamRepo;

        public AdminAboutController(IAboutRepository aboutRepo, ITeamMemberRepository teamRepo)
        {
            _aboutRepo = aboutRepo;
            _teamRepo = teamRepo;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Content("❌ You are not authorized to access this page.");
            }
            var aboutVm = _aboutRepo.GetAboutPage();
            var teamMembers = _teamRepo.GetTeamMembers();

            var viewModel = new AboutPageViewModel
            {
                About = aboutVm,
                TeamMembers = teamMembers
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAbout(AboutViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            await _aboutRepo.SaveAbout(model);
            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> SaveTeamMember(TeamMemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _teamRepo.SaveTeamMember(model);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteTeamMember(int id)
        {
            _teamRepo.DeleteTeamMember(id);
            return RedirectToAction("Index");
        }
    }
}
