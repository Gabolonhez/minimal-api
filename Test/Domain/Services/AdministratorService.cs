using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Services;
using minimal_api.Domain.Enums;
using minimal_api.Infraestructure.Db;

namespace Test.Domain.Services
{
    [TestClass]
    public class AdministratorServiceTests
    {
        private ApplicationDbContext CreateContextTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique database name
                .Options;

            return new ApplicationDbContext(options);
        }

        [TestMethod]
        public void SaveAdministratorTest()
        {
            // Arrange
            var context = CreateContextTest();
            
            var adm = new Administrator
            {
                Email = "test@gmail.com",
                Password = "test123",
                Profile = Profile.Adm // Use enum value
            };

            var administratorService = new AdministratorService(context);

            // Act
            administratorService.Insert(adm);
            var dbAdm = administratorService.FindById(adm.Id);

            // Assert
            Assert.AreEqual(1, dbAdm?.Id);
            Assert.AreEqual("test@gmail.com", dbAdm?.Email);
            Assert.AreEqual("test123", dbAdm?.Password);   
            Assert.AreEqual(Profile.Adm, dbAdm?.Profile);
        }
    }
}