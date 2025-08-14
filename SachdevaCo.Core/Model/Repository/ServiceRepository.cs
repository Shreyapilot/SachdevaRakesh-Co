using System.Collections.Generic;
using System.Linq;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;

namespace SachdevaCo.Core.Model.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SachdevaCoDbContext _context;

        public ServiceRepository(SachdevaCoDbContext context)
        {
            _context = context;
        }

        public List<ServiceViewModel> GetAllServices()
        {
            return _context.Services
                .Select(s => new ServiceViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Duration = s.Duration,
                    Price = s.Price ?? 0,
                    ImageUrl = s.ImageUrl
                }).ToList();
        }

        public void AddOrUpdateService(ServiceViewModel model)
        {
            if (model.Id == null || model.Id == 0)
            {
                var newService = new Service
                {
                    Title = model.Title,
                    Description = model.Description,
                    Duration = model.Duration,
                    Price = model.Price ?? 0,
                    ImageUrl = model.ImageUrl
                };
                _context.Services.Add(newService);
            }
            else
            {
                var existing = _context.Services.FirstOrDefault(t => t.Id == model.Id);
                if (existing != null)
                {
                    existing.Title = model.Title;
                    existing.Description = model.Description;
                    existing.Duration = model.Duration;
                    existing.Price = model.Price ?? 0;
                    existing.ImageUrl = model.ImageUrl;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteService(int id)
        {
            var existing = _context.Services.FirstOrDefault(t => t.Id == id);
            if (existing != null)
            {
                _context.Services.Remove(existing);
                _context.SaveChanges();
            }
        }
    }
}
