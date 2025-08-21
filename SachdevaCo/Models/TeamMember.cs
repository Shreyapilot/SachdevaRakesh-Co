using System;
using System.Collections.Generic;

namespace SachdevaCo.Models;

public partial class TeamMember
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Position { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
