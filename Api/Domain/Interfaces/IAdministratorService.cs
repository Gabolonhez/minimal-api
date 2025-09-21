using minimal_api.Domain.DTOs;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IAdministratorService
    {
        Administrator? Login(LoginDTO loginDTO);

        Administrator Insert(Administrator administrator);

        Administrator FindById(int id);
        List<Administrator> All(int? page);

    }
}
