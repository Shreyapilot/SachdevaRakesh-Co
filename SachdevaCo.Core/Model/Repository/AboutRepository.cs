using Microsoft.EntityFrameworkCore;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SachdevaCo.Core.Model.Repository
{
    public class AboutRepository : IAboutRepository
    {
        private readonly SachdevaCoDbContext _context;

        public AboutRepository(SachdevaCoDbContext context)
        {
            _context = context;
        }

        public AboutViewModel GetAboutPage()
        {
            var about = _context.AboutPages.FirstOrDefault();
            return about != null
                ? new AboutViewModel
                {
                    Id = about.Id,
                    Title = about.Title,
                    Description = about.Description,
                    ImageUrl = about.ImageUrl,
                    CreatedAt = about.CreatedAt,
                    UpdatedAt = about.UpdatedAt
                }
                : new AboutViewModel();
        }

        public async Task SaveAbout(AboutViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/about");

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                model.ImageUrl = "/images/about/" + uniqueFileName;
            }

            var about = await _context.AboutPages.FirstOrDefaultAsync(a => a.Id == model.Id);

            if (about == null)
            {
                // Insert new record
                about = new AboutPage
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.AboutPages.Add(about);
            }
            else
            {
                about.Title = model.Title;
                about.Description = model.Description;

                if (!string.IsNullOrEmpty(model.ImageUrl))  
                    about.ImageUrl = model.ImageUrl;

                about.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        public class TeamMemberRepository : ITeamMemberRepository
        {
            private readonly SachdevaCoDbContext _context;

            public TeamMemberRepository(SachdevaCoDbContext context)
            {
                _context = context;
            }

            public List<TeamMemberViewModel> GetTeamMembers()
            {
                return _context.TeamMembers
                    .Select(tm => new TeamMemberViewModel
                    {
                        Id = tm.Id,
                        Name = tm.Name,
                        Position = tm.Position,
                        Email = tm.Email,
                        Phone = tm.Phone,
                        Description = tm.Description,
                        ImageUrl = tm.ImageUrl,
                        CreatedAt = tm.CreatedAt,
                        UpdatedAt = tm.UpdatedAt
                    }).ToList();
            }

            public async Task SaveTeamMember(TeamMemberViewModel model)
            {
                // ✅ Step 1: Save image if uploaded
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/team");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = "/images/team/" + fileName;
                }

                // ✅ Step 2: Save to DB
                if (model.Id == null || model.Id == 0)
                {
                    var newMember = new TeamMember
                    {
                        Name = model.Name,
                        Position = model.Position,
                        Email = model.Email,
                        Phone = model.Phone,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.TeamMembers.Add(newMember);
                }
                else
                {
                    var existing = _context.TeamMembers.FirstOrDefault(t => t.Id == model.Id);
                    if (existing != null)
                    {
                        existing.Name = model.Name;
                        existing.Position = model.Position;
                        existing.Email = model.Email;
                        existing.Phone = model.Phone;
                        existing.Description = model.Description;

                        // sirf image replace jab new upload ho
                        if (!string.IsNullOrEmpty(model.ImageUrl))
                        {
                            existing.ImageUrl = model.ImageUrl;
                        }

                        existing.UpdatedAt = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();
            }


            public void DeleteTeamMember(int id)
            {
                var teamMember = _context.TeamMembers.FirstOrDefault(t => t.Id == id);
                if (teamMember != null)
                {
                    _context.TeamMembers.Remove(teamMember);
                    _context.SaveChanges();
                }
            }
        }
    }

}