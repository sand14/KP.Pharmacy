using KP.Common.Model.Automapper;
using KP.Common.Model.Models;
using KP.Core.Data;
using KP.Core.DomainModels;
using KP.Services.User;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace KP.Tests
{
    public class UserServiceTests
    {
        private EFCoreRepository<User> userRepository;

        private UserService GetService()
        {
            return new(userRepository);
        }

        private UserModel CreateUserModel(string username, string password)
        {
            UserModel user = new()
            {
                Username = username,
                Password = password
            };

            return user;
        }

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PharmacyContext>();
            options.UseSqlServer("Server=.\\SQLEXPRESS;Database=Pharmacy;Trusted_Connection=True;");
            PharmacyContext _dbContext = new PharmacyContext(options.Options);

            //Set up Repos
            userRepository = new(_dbContext);


            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void CreateUserTest()
        {
            //arrange
            UserService service = GetService();
            Guid createdUserId = Guid.Empty;
            try
            {
                UserModel user = CreateUserModel("username1", "password1");

                //act
                UserModel createdUser = service.CreateUser(user);
                createdUserId = createdUser.UserId;

                //assert
                Assert.That(createdUser != null);
                Assert.That(service.PasswordVerify("username1", "password1"));

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.DeleteUser(createdUserId);
            }
        }

        [Test]
        public void DeleteUserTest()
        {
            //arrange
            UserService service = GetService();
            UserModel user = CreateUserModel("TestUsername","TestPassword");
            UserModel createdUser = service.CreateUser(user);

            //act
            service.DeleteUser(createdUser.UserId);

            //assert
            var deletedUser = service.GetUserById(createdUser.UserId);
            Assert.That(deletedUser == null);
        }


    }
}
