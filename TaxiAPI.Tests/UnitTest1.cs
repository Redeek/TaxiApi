using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxiApi.Entities;
using TaxiApi.Exceptions;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiAPI.Tests
{
    public class AccountServiceTests
    {
        private AccountService _service;
        private TaxiDbContext _context;

        public AccountServiceTests()
        {
            var opt = new DbContextOptionsBuilder<TaxiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new TaxiDbContext(opt);
            _service = new AccountService(_context, null, null, null);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }

        private User CreateTestUser(int id, int roleId)
        {
            return new User
            {
                Id = id,
                RoleId = roleId,
                Email = $"test{id}@example.com",
                Name = "TestName",
                Password = "TestPassword",
                PhoneNumber = "123456789",
                Surname = "TestSurname"
            };
        }


        [Fact]
        public void DeleteUser_UserNotFound_ThrowsNotFoundException()
        {
            int nonExistingUserId = 9999;

            Assert.Throws<NotFoundException>(() => _service.DeleteUser(nonExistingUserId));
        }

        [Fact]
        public void DeleteUser_UserNotDriver_ThrowsUserInvalidOperationException()
        {
            var user = CreateTestUser(1,2);
            _context.Users.Add(user);
            _context.SaveChanges();


            Assert.Throws<UserInvalidOperationException>(() => _service.DeleteUser(user.Id));
        }

        [Fact]
        public void DeleteUser_ValidDriver_DeleteUser()
        {
            var user = CreateTestUser(1,1);
            _context.Users.Add(user);
            _context.SaveChanges();


            _service.DeleteUser(user.Id);

            var deletedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.Null(deletedUser);
        }




    }


}

