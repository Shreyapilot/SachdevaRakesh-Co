using SachdevaCo.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.IRepository
{
    public interface IServiceBookingRepository
    {
        List<ServiceBookingViewModel> GetAllBooking();
        void AddOrUpdateBooking(ServiceBookingViewModel model);
        void DeleteBooking(int id);
    }
}
