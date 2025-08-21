using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }   
        public string? FilePath { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
