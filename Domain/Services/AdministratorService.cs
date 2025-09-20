using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using minimal_api.Infraestructure.Db;
using minimal_api.Domain.Services;
using minimal_api.Domain.DTOs;

using Microsoft.EntityFrameworkCore;


namespace minimal_api.Domain.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly DbContext _context;

        public AdministratorService(DbContext db)
        {
            _context = db;
        }

        public Administrator? Login(LoginDTO loginDTO)
        {
            var adm = _context.Set<Administrator>().Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
            return adm;
        }
    }
}
