using System.Collections.Generic;
using SachdevaCo.Core.Model.ViewModels;

namespace SachdevaCo.Core.Model.IRepository
{
    public interface IAboutRepository
    {
        AboutViewModel GetAboutPage();

        Task SaveAbout(AboutViewModel model);
    }

    public interface ITeamMemberRepository
    {
        List<TeamMemberViewModel> GetTeamMembers();
        Task SaveTeamMember(TeamMemberViewModel model);
        void DeleteTeamMember(int id);
    }
}
