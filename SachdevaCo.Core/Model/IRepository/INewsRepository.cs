using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SachdevaCo.Core.Model.ViewModels;


namespace SachdevaCo.Core.Model.IRepository
{
    public interface INewsRepository
    {
        List<NewsViewModel> GetAllNews();
        Task AddNewsAsync(NewsViewModel model);
        Task DeleteNewsAsync(int id);
    }
}
