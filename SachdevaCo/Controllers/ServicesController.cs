using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepo;

        public ServicesController(IServiceRepository serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public IActionResult Index()
        {
            var services = _serviceRepo.GetAllServices();
            return View(services);
        }

       
       
    }
}
