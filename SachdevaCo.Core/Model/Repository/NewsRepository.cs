
using Microsoft.EntityFrameworkCore;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;

public class NewsRepository : INewsRepository
{

    private readonly SachdevaCoDbContext _context;

    public NewsRepository(SachdevaCoDbContext context)
    {
        _context = context;
    }
    public List<NewsViewModel> GetAllNews()
    {
        return _context.News
            .Select(a => new NewsViewModel
            {
                Id = a.Id,
                Title = a.Title,
                FilePath = a.FilePath,
                Category = a.Category,
                Descriptions = a.Descriptions,
                CreatedDate = a.CreatedDate,
                ImageUrl = a.ImageUrl,
            })
            .ToList();
    }

    public async Task AddNewsAsync(NewsViewModel model)
    {
        if (model.File == null || string.IsNullOrEmpty(model.Title))
            throw new ArgumentException("Please provide a title and PDF file.");

        var extension = Path.GetExtension(model.File.FileName).ToLower();
        if (extension != ".pdf")
            throw new ArgumentException("Only PDF files are allowed.");

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/News");
        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + extension;
        var filePath = Path.Combine(uploadFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        model.FilePath = "/uploads/News/" + uniqueFileName;

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadImageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/News");

            if (!Directory.Exists(uploadImageFolder))
                Directory.CreateDirectory(uploadImageFolder);

            var uniqueImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
            var ImagefilePath = Path.Combine(uploadImageFolder, uniqueImageFileName);

            using (var stream = new FileStream(ImagefilePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

            model.ImageUrl = "/uploads/News/" + uniqueImageFileName;
        }

        var news = new News
        {
            Title = model.Title,
            FilePath = model.FilePath,
            Category = model.Category,
            Descriptions = model.Descriptions,
            CreatedDate = model.CreatedDate,
            ImageUrl = model.ImageUrl
        };

        _context.News.Add(news);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteNewsAsync(int id)
    {
        var news = await _context.News.FirstOrDefaultAsync(t => t.Id == id);
        if (news != null)
        {
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }
    }

}

