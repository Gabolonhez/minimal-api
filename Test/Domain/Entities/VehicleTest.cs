using MiniamlApi.Domain.Entities;

namepasce Test.Domain.Entities;

[TestClass]

public class VehicleTest
{
    [TestMethod]
    public void GetSetPropritiesTest()
    {

        // Arrange
        var vehicle = new Vehcile();

        // Act
        vehicle.Id = 1;
        vehicle.Name = "Test name";
        vehicle.Brand = "Test";
        vehicle.Year = "2000";

        // Assert
        Assert.AreEqual(1, vehicle.Id);
        Assert.AreEqual("Test name", vehicle.Name);
        Assert.AreEqual("Test", vehicle.Brand);
        Assert.AreEqual("2000", vehicle.Year);

    }
}
