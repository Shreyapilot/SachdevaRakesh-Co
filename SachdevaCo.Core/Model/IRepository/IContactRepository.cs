using SachdevaCo.Core.Model.ViewModels;
using System;
using SachdevaCo.Core.Models;

namespace SachdevaCo.Core.Model.IRepository
{
    public interface IContactRepository
    {
        void AddMessage(ContactMessage message);
    }
}
