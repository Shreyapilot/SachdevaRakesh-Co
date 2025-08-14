using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class ServiceViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
