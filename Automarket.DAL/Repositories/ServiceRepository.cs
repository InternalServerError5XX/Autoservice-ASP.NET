using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class ServiceRepository : IBaseRepository<Service>
    {
        private readonly ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Service entity)
        {
            await _db.Services.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Service entity)
        {
            _db.Services.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Service>> GetAll()
        {
            return await Task.FromResult(_db.Services);
        }

        public async Task<Service> Update(Service entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
