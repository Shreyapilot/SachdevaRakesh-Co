using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly SachdevaCoDbContext _context;

        public ContactRepository(SachdevaCoDbContext context)
        {
            _context = context;
        }

        public void AddMessage(ContactMessage message)
        {
            _context.ContactMessages.Add(message);
            _context.SaveChanges();
        }
    }

}
