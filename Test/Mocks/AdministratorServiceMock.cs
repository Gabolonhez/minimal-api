using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Enums;

namespace Test.Mocks;

public class AdministratorServiceMock : IAdministratorService
{
    private static List<Administrator> administrators = new List<Administrator>() 
    {
        new Administrator
        {
            Id = 1,
            Email = "adm@test.com",
            Password = "test123",
            Profile = Profile.Adm
        },
        new Administrator
        {
            Id = 2,
            Email = "test2@gmail.com",
            Password = "test1234",
            Profile = Profile.Editor
        },
    };

    public Administrator? FindById(int id)
    {
        return administrators.Find(a => a.Id == id);
    }

    public Administrator Insert(Administrator administrator)
    {
        administrator.Id = administrators.Count + 1;
        administrators.Add(administrator);

        return administrator;
    }

    public Administrator? Login(LoginDTO loginDTO)
    {
        return administrators.Find(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password);
    }

    public List<Administrator> All(int? page)
    {
        return administrators;
    }
}