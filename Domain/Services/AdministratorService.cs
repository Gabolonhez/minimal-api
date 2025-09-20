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

        public Administrator Insert(Administrator administrator)
        {
            _context.Set<Administrator>().Add(administrator);
            _context.SaveChanges();
            return administrator;
        }

        public List <Administrator> GetAllAdministrators(int? page)
        {
            var query = _context.Set<Administrator>().AsQueryable();

            int itensPerPage = 10;

            if (page != null)
                query = query.Skip(((int)page - 1) * itensPerPage).Take(itensPerPage);

            return query.ToList();
        }

        public Administrator FindById(int id)
        {
            var adm = _context.Set<Administrator>().Where(a => a.Id == id).FirstOrDefault();
            return adm;
        }

        public List<Administrator> All(int? page)
        {
            return GetAllAdministrators(page);
        }

    }
}
