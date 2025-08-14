using System.ComponentModel.DataAnnotations;


namespace SachdevaCo.Core.Model.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}