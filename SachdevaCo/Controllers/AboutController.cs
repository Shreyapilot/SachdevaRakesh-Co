using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Web.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutRepository _aboutRepo;
        private readonly ITeamMemberRepository _teamRepo;

        public AboutController(IAboutRepository aboutRepo, ITeamMemberRepository teamRepo)
        {
            _aboutRepo = aboutRepo;
            _teamRepo = teamRepo;
        }

        public IActionResult Index()
        {
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
        public IActionResult SaveAbout(AboutViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _aboutRepo.SaveAbout(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveTeamMember(TeamMemberViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _teamRepo.SaveTeamMember(model);
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
