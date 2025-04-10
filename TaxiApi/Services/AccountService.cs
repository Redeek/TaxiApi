using Microsoft.AspNetCore.Identity;
using TaxiApi.Entities;
using TaxiApi.Models;

namespace TaxiApi.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
    }

    public class AccountService: IAccountService
    {
        private readonly TaxiDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(TaxiDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void RegisterUser(RegisterUserDto dto)
        {

            var newUser = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.Password = hashedPassword;

            _context.Users.Add(newUser);
            _context.SaveChanges();

        }
    }
}
