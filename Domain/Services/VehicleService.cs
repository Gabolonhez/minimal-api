using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;

namespace minimal_api.Domain.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DbContext _context;

        public VehicleService(DbContext db)
        {
            _context = db;
        }

        public List<Vehicle> All(int page = 1, string? name = null, string? brand = null)
        {
            var query = _context.Set<Vehicle>().AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(v => v.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            int itensPerPage = 10;

            query = query.Skip((page - 1) * itensPerPage).Take(itensPerPage);

            return query.ToList();
        }

        public void Insert(Vehicle vehicle)
        {
            _context.Set<Vehicle>().Add(vehicle);
            _context.SaveChanges();
        }

        public void Update(Vehicle vehicle)
        {
           _context.Set<Vehicle>().Update(vehicle);
           _context.SaveChanges();
        }
        public void Delete (Vehicle vehicle) 
        {
            _context.Set<Vehicle>().Remove(vehicle);      
            _context.SaveChanges();
        }

        public Vehicle? GetById(int id)
        {
            return _context.Set<Vehicle>().Where(v => v.Id == id).FirstOrDefault();
        }
    }
}
