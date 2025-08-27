using System;
using System.Collections.Generic;

namespace SachdevaCo.Models;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Category { get; set; }

    public string? FilePath { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Descriptions { get; set; }
}
