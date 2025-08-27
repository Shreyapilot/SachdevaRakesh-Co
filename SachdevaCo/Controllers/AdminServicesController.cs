using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Controllers
{
    public class AdminServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IServiceBookingRepository _bookingRepo;

        public AdminServicesController(IServiceRepository serviceRepo, IServiceBookingRepository bookingRepo)
        {
            _serviceRepo = serviceRepo;
            _bookingRepo = bookingRepo;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return Content("❌ You are not authorized to access this page.");
            }

            var vm = new ServiceDashboardViewModel
            {
                Services = _serviceRepo.GetAllServices(),
                Bookings = _bookingRepo.GetAllBooking()
            };

            return View(vm);
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
        [HttpPost]
        public IActionResult CreateBooking(ServiceBookingViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _bookingRepo.AddOrUpdateBooking(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteBooking(int id)
        {
            _bookingRepo.DeleteBooking(id);
            return RedirectToAction("Index");
        }
    }
}
