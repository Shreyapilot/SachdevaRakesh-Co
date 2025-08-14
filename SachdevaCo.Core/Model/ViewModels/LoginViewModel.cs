using System.ComponentModel.DataAnnotations;

namespace SachdevaCo.Core.Model.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? UserName { get; set; }
        public int? UserId { get; set; }
        public string? Role { get; set; }

    }
}
