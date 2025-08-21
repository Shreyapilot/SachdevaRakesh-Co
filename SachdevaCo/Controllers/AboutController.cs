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

       
    }
}
