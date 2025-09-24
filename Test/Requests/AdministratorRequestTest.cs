using System.Text.Json;
using System.Threading.Tasks;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.ModelsViews;
using Test.Helpers;
using System.Text;
using System.Net;

namespace Test.Request.Entities;

[TestClass]

public class AdministratorRequestTest
{
    [ClassInitialize]

    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]

    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }

    [TestMethod]
    public async Task GetSetPropritiesTest()
    {

        // Arrange
        var loginDTO = new LoginDTO { 
            Email = "adm@test.com",
            Password = "test123"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

        // Act

        var response = await Setup.Client.PostAsync("/administrators/login", content);

        // Assert

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();

        var loggedAdm = JsonSerializer.Deserialize<LoggedAdmin>(result, new JsonSerializerOptions
        { 
            PropertyNameCaseInsensitive = true
        });
        
        Assert.IsNotNull(loggedAdm?.Email);
        Assert.IsNotNull(loggedAdm?.Token);
        Assert.IsNotNull(loggedAdm?.Profile);

    }
}
