using Microsoft.VisualStudio.TestTools.UnitTesting;
using minimal_api.Domain.Entities;

namespace Test.Domain.Entities;

[TestClass]
public class VehicleTest
{
    [TestMethod]
    public void GetSetPropertiesTest()
    {
        // Arrange
        var vehicle = new Vehicle();

        // Act
        vehicle.Id = 1;
        vehicle.Name = "Test name";
        vehicle.Brand = "Test";
        vehicle.Year = 2000;

        // Assert
        Assert.AreEqual(1, vehicle.Id);
        Assert.AreEqual("Test name", vehicle.Name);
        Assert.AreEqual("Test", vehicle.Brand);
        Assert.AreEqual(2000, vehicle.Year);
    }
}
