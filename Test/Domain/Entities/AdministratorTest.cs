using minimal_api.Domain.Entities;
using minimal_api.Domain.Enums;

namespace Test.Domain.Entities;

[TestClass]

public class AdministratorTest
{
    [TestMethod]
    public void GetSetPropritiesTest()
    {

        // Arrange
        var adm = new Administrator();

        // Act
        adm.Id = 1;
        adm.Email = "test@test.com";
        adm.Password = "Test123";
        adm.Profile = Profile.Adm;

        // Assert
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("test@test.com", adm.Email);
        Assert.AreEqual("Test123", adm.Password);
        Assert.AreEqual(Profile.Adm, adm.Profile);

    }
}
