using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.Repository
{
     public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly SachdevaCoDbContext _context;

        public ServiceBookingRepository(SachdevaCoDbContext context)
        {
            _context = context;
        }
        public List<ServiceBookingViewModel> GetAllBooking()
        {
            return _context.ServiceBookings
                .Select(b => new ServiceBookingViewModel
                {
                    BookingID = b.BookingID,
                    Name = b.Name ?? string.Empty,
                    Email = b.Email ?? string.Empty,
                    Phone = b.Phone ?? string.Empty,
                    CompanyName = b.CompanyName ?? string.Empty,
                    PreferredDate = b.PreferredDate,      
                    PreferredTime = b.PreferredTime,      
                    Message = b.Message ?? string.Empty,
                    ServiceID = b.ServiceID
                })
                .ToList();
        }


        public void AddOrUpdateBooking(ServiceBookingViewModel model)
        {
            if (model.BookingID == null || model.BookingID == 0) 
            {
                var newBooking = new ServiceBookings
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    CompanyName = model.CompanyName,
                    PreferredDate = model.PreferredDate,
                    PreferredTime = model.PreferredTime,
                    Message = model.Message,
                    ServiceID = model.ServiceID,
                };

                _context.ServiceBookings.Add(newBooking);
            }
            else 
            {
                var existing = _context.ServiceBookings.FirstOrDefault(b => b.BookingID == model.BookingID);
                if (existing != null)
                {
                    existing.Name = model.Name;
                    existing.Email = model.Email;
                    existing.Phone = model.Phone;
                    existing.CompanyName = model.CompanyName;
                    existing.PreferredDate = model.PreferredDate;
                    existing.PreferredTime = model.PreferredTime;
                    existing.Message = model.Message;
                    existing.ServiceID = model.ServiceID;
                }
            }

            _context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var existing = _context.ServiceBookings.FirstOrDefault(t => t.ServiceID == id);
            if (existing != null)
            {
                _context.SaveChanges();
            }
        }
    }
}

