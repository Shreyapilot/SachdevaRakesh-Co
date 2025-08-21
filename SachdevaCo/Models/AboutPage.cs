using System;
using System.Collections.Generic;

namespace SachdevaCo.Models;

public partial class AboutPage
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ImageUrl { get; set; }
}
