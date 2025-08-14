

using System.Collections.Generic;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;

namespace SachdevaCo.Core.Model.IRepository
{
    public interface IServiceRepository
    {
        List<ServiceViewModel> GetAllServices();
        void AddOrUpdateService(ServiceViewModel model);
        void DeleteService(int id);
    }
}

