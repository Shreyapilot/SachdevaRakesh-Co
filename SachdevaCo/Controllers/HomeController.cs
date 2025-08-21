using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace SachdevaCo.Controllers;
using SachdevaCo.Core.Model;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IServiceRepository _serviceRepo;
    private readonly IAboutRepository _aboutRepo;
    private readonly IArticleRepository _articleRepo;




    public HomeController(ILogger<HomeController> logger, IServiceRepository serviceRepo, IAboutRepository aboutRepo, IArticleRepository articleRepo)
    {
        _logger = logger;
        _serviceRepo = serviceRepo;
        _aboutRepo = aboutRepo;
        _articleRepo = articleRepo;
    }

    public IActionResult Index()
    {
        var services = _serviceRepo.GetAllServices();

        var articles = _articleRepo.GetAllArticles();


        var aboutVm = _aboutRepo.GetAboutPage();


        var viewModel = new HomeViewModel
        {
            Services = services,
            About = aboutVm,
            Article = articles
        };

        return View(viewModel);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
