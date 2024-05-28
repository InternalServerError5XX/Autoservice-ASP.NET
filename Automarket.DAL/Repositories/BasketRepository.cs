using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly ApplicationDbContext _db;

        public BasketRepository (ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Basket entity)
        {
            await _db.Basket.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Basket entity)
        {
            _db.Basket.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Basket>> GetAll()
        {
            return await Task.FromResult(_db.Basket);
        }

        public async Task<Basket> Update(Basket entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
