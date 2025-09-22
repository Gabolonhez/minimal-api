using MinimalApi.Domai.Entities;

namespace Test.Domain.Services

[TestClass]

public class AdministratorServiceTests
{
    [TestMethod]

    public void SaveAdministratorTest()
    {

        private DbContext CreateContextTest()
        {
            var options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
    }

        // Arrange
        var adm = new Administrator();
        adm.Id = 1;
        adm.Email = "test@gmail.com";
        adm.Password = "test123";
        adm.Profile = "Adm";

        //Act
        var context = CreateContextTest();

        // Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("test@gmail.com", adm.Email);
        Assert.AreEqual("test123", adm.Password);   
        Assert.AreEqual("Adm", adm.Profile);

    }
}