using BankApp.Data.DataModels;
using BankApp.Data.Repos;
using BankApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankApp.UnitTests
{
    public class Tests
    {
        private DbContextOptions<BankAppDBContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BankAppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Test]
        public async Task Login_Failed_Customer()
        {
            using (var context = new BankAppDBContext(_dbContextOptions))
            {
                //Arrange
                var configuration = new ConfigurationBuilder()
                               .AddInMemoryCollection(new Dictionary<string, string>
                               {
                                   { "AppSettings:Token", "your_secret_token_value_here" }
                               })
                               .Build();
                //Act
                AuthRepo authRepo = new AuthRepo(context, configuration);
                var result = await authRepo.Login("testuser", "password");

                //Assert
                Assert.IsFalse(result.Success);
            }
        }

        [Test]
        public async Task Login_Successful_Customer()
        {
            using (var context = new BankAppDBContext(_dbContextOptions))
            {
                // Arrange
                var configuration = new ConfigurationBuilder()
                               .AddInMemoryCollection(new Dictionary<string, string>
                               {
                                   { "AppSettings:Token", "your_secret_token_value_here" }
                               })
                               .Build(); 

                var customerId = 1;
                var username = "testuser";
                var password = "password";

                context.Customers.Add(new Customer { CustomerId = customerId, Username = username, Password = password });
                context.SaveChanges();
                var authRepo = new AuthRepo(context, configuration);

                // Act
                var result = await authRepo.Login(username, password);

                // Assert
                Assert.IsTrue(result.Success);
            }
        }

        [Test]
        public async Task Login_Wrong_Password()
        {
            using (var context = new BankAppDBContext(_dbContextOptions))
            {
                // Arrange
                var configuration = new ConfigurationBuilder().Build(); // You can configure the configuration as needed
                var authRepo = new AuthRepo(context, configuration);

                var username = "testuser";
                var password = "password";

                context.Customers.Add(new Customer { Username = username, Password = password });
                context.SaveChanges();

                // Act
                var result = await authRepo.Login(username, "wrongpassword");

                // Assert
                Assert.IsFalse(result.Success);
                Assert.AreEqual("User not found. Wrong username or password.", result.Message);
            }
        }
    }
}