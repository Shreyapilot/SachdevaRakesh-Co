using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class ServiceBookingViewModel
    {
        public int BookingID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }
        public DateTime PreferredDate { get; set; } = DateTime.Now;
        public TimeSpan PreferredTime { get; set; } = DateTime.Now.TimeOfDay;
        public string? Message { get; set; }
        public int ServiceID { get; set; }
    }
}
