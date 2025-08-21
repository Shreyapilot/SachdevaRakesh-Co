using System;
using System.Collections.Generic;

namespace SachdevaCo.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? Role { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public string? Token { get; set; }

    public DateTime? TokenExpiryDate { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public DateTime CreatedDate { get; set; }
}
