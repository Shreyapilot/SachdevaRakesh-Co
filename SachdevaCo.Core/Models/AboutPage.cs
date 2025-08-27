using System;
using System.Collections.Generic;

namespace SachdevaCo.Core.Models;

public partial class AboutPage
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string FrontDescriptions { get; set; }
    public string Mission { get; set; } 
    public string Value { get; set; } 
    public string Vision { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? ImageUrl { get; set; }
}
