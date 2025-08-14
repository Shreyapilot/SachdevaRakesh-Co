using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class ContactMessageViewModel
    {
        
        public string FirstName { get; set; }

      
        public string LastName { get; set; }

       
        public string Email { get; set; }

  
        public string Subject { get; set; }

        
        public string Message { get; set; }
    }
}
