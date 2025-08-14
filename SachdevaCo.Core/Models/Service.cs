using System;
using System.Collections.Generic;

namespace SachdevaCo.Core.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Duration { get; set; }

    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
