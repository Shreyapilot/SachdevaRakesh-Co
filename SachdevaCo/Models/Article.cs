using System;
using System.Collections.Generic;

namespace SachdevaCo.Models;

public partial class Article
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }
}
