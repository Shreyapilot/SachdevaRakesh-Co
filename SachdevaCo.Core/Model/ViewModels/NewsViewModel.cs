using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class NewsViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public IFormFile? File { get; set; }



    }
}
