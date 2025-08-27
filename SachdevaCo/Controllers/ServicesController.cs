using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepo;
        private readonly IServiceBookingRepository _bookingRepo;


        public ServicesController(IServiceRepository serviceRepo, IServiceBookingRepository bookingRepo)
        {
            _serviceRepo = serviceRepo;
            _bookingRepo = bookingRepo;
        
        }
        public IActionResult Index()
        {
            var services = _serviceRepo.GetAllServices();

            var model = new ServiceDashboardViewModel
            {
                Services = services   
            };

            return View(model);
        }

        public IActionResult CreateBooking(ServiceBookingViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            _bookingRepo.AddOrUpdateBooking(model);
            return RedirectToAction("Index");
        }


    }
}
