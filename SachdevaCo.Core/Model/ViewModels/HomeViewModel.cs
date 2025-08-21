using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class HomeViewModel
    {
        public List<ServiceViewModel> Services { get; set; }
        public AboutViewModel About { get; set; }

        public List<ArticleViewModel> Article { get; set; }
    }
}
