using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class MaintenanceRepository : IBaseRepository<Maintenance>
    {
        private readonly ApplicationDbContext _db;

        public MaintenanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Maintenance entity)
        {
            await _db.Maintenances.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Maintenance entity)
        {
            _db.Maintenances.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Maintenance>> GetAll()
        {
            return await Task.FromResult(_db.Maintenances);
        }

        public async Task<Maintenance> Update(Maintenance entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
