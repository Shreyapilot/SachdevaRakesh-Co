using System;
using System.Collections.Generic;
using System.Linq;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;

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
                    CreatedAt = about.CreatedAt,
                    UpdatedAt = about.UpdatedAt
                }
                : new AboutViewModel();
        }

        public void SaveAbout(AboutViewModel model)
        {
            var about = _context.AboutPages.FirstOrDefault();
            if (about == null)
            {
                about = new AboutPage
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.AboutPages.Add(about);
            }
            else
            {
                about.Title = model.Title;
                about.Description = model.Description;
                about.UpdatedAt = DateTime.Now;
            }
            _context.SaveChanges();
        }
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

        public void SaveTeamMember(TeamMemberViewModel model)
        {
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
                    existing.ImageUrl = model.ImageUrl;
                    existing.UpdatedAt = DateTime.Now;
                }
            }
            _context.SaveChanges();
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
