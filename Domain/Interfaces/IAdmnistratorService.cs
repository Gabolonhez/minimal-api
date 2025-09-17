using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IAdmnistratorService
    {
        Admnistrator? Login(LoginDTO loginDTO);
    }
}
