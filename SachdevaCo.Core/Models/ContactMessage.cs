using System;
using System.Collections.Generic;

namespace SachdevaCo.Core.Models;

public partial class ContactMessage
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Subject { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
