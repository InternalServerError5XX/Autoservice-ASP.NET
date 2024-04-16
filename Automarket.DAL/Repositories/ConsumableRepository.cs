using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class ConsumableRepository : IBaseRepository<Consumable>
    {
        private readonly ApplicationDbContext _db;

        public ConsumableRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Consumable entity)
        {
            await _db.Consumables.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Consumable entity)
        {
            _db.Consumables.Remove(entity);
            //await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Consumable>> GetAll()
        {
            return await Task.FromResult(_db.Consumables);
        }

        public async Task<Consumable> Update(Consumable entity)
        {
            _db.Consumables.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
