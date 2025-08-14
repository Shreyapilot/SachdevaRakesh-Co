using SachdevaCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class AboutPageViewModel
    {
        public AboutViewModel About { get; set; }
        public List<TeamMemberViewModel> TeamMembers { get; set; } = new List<TeamMemberViewModel>();

    }
}
