using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Models
{
    public partial class ServiceBookings
    {

        public int BookingID { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public string? Message { get; set; } 
        public int ServiceID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;    
        
    }
}
