using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using minimal_api.Infraestructure.Db;
using minimal_api.Domain.Services;
using minimal_api.Domain.DTOs;

using Microsoft.EntityFrameworkCore;


namespace minimal_api.Domain.Services
{
    public class AdmnistratorService : IAdmnistratorService
    {
        private readonly DbContext _context;

        public AdmnistratorService(DbContext db)
        {
            _context = db;
        }

        public Admnistrator? Login(LoginDTO loginDTO)
        {
            var adm = _context.Set<Admnistrator>().Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
            return adm;
        }
    }
}
