using minimal_api.Domain.Interfaces;
using minimal_api.Domain.Entities;
using minimal_api.Infraestructure.Db;
using minimal_api.Domain.DTOs;
using minimal_api.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace minimal_api.Domain.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly ApplicationDbContext _context; // Changed from DbContext to ApplicationDbContext

        public AdministratorService(ApplicationDbContext context) // Changed parameter type
        {
            _context = context;
        }

        public Administrator? Login(LoginDTO loginDTO)
        {
            var adm = _context.Administrators.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
            return adm;
        }

        public Administrator Insert(Administrator administrator)
        {
            _context.Administrators.Add(administrator); // Use the specific DbSet
            _context.SaveChanges();
            return administrator;
        }

        public List<Administrator> GetAllAdministrators(int? page)
        {
            var query = _context.Administrators.AsQueryable(); // Use the specific DbSet

            int itensPerPage = 10;

            if (page != null)
                query = query.Skip(((int)page - 1) * itensPerPage).Take(itensPerPage);

            return query.ToList();
        }

        public Administrator? FindById(int id) // Made return type nullable to match usage
        {
            return _context.Administrators.Where(v => v.Id == id).FirstOrDefault();
        }

        public List<Administrator> All(int? page)
        {
            return GetAllAdministrators(page);
        }
    }
}

