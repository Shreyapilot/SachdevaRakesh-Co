using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArticleRepository
{
    List<ArticleViewModel> GetAllArticles();
    Task AddArticlesAsync(ArticleViewModel model);
    Task DeleteArticlesAsync(int id);

}
