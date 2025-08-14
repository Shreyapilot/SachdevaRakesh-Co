using SachdevaCo.Core.Models;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SachdevaCo.Core.Model.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SachdevaCoDbContext _context;

        public LoginRepository(SachdevaCoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public LoginViewModel AuthenticateUser(string email, string password)
        {
            var normalizedEmail = email.ToLower();

            var user2 = _context.Users;

            var user = _context.Users
                .FirstOrDefault(u => u.Email != null && u.Email.ToLower() == normalizedEmail);


            if (user == null || user.PasswordHash == null || user.PasswordSalt == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            user.LastLoginDate = DateTime.Now;
            _context.SaveChanges();

            return new LoginViewModel
            {
                UserId = user.Id,
                Role = user.Role,
                Email = user.Email,
                UserName = user.Username
            };
        }

        public void RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
