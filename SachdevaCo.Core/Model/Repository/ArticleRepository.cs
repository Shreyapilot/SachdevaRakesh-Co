using Microsoft.EntityFrameworkCore;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;

public class ArticleRepository : IArticleRepository
{
    private readonly SachdevaCoDbContext _context;

    public ArticleRepository(SachdevaCoDbContext context)
    {
        _context = context;
    }

    public List<ArticleViewModel> GetAllArticles()
    {
        return _context.Articles
            .Select(a => new ArticleViewModel
            {
                Id = a.Id,
                Title = a.Title,
                FilePath = a.FilePath
            })
            .ToList();
    }

    public async Task AddArticlesAsync(ArticleViewModel model)
    {
        if (model.File == null || string.IsNullOrEmpty(model.Title))
            throw new ArgumentException("Please provide a title and PDF file.");

        var extension = Path.GetExtension(model.File.FileName).ToLower();
        if (extension != ".pdf")
            throw new ArgumentException("Only PDF files are allowed.");

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/articles");
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        model.FilePath = "/uploads/articles/" + uniqueFileName;

        var article = new Article
        {
            Title = model.Title,
            FilePath = model.FilePath
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteArticlesAsync(int id)
    {
        var article = await _context.Articles.FirstOrDefaultAsync(t => t.Id == id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}
