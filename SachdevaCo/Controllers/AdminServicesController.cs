using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Controllers
{
    

    public class AdminServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepo;

        public AdminServicesController(IServiceRepository serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Content("❌ You are not authorized to access this page.");
            }
            var services = _serviceRepo.GetAllServices();
            return View(services);
        }

        [HttpPost]
        public IActionResult Create(ServiceViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _serviceRepo.AddOrUpdateService(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _serviceRepo.DeleteService(id);
            return RedirectToAction("Index");
        }
    }
}
