using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class ServiceDashboardViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
        public List<ServiceBookingViewModel> Bookings { get; set; }
    }
}
