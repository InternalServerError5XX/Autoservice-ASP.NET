using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class WishlistRepository : IBaseRepository<Wishlist>
    {
        private readonly ApplicationDbContext _db;

        public WishlistRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Wishlist entity)
        {
            await _db.Wishlist.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Wishlist entity)
        {
            _db.Wishlist.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Wishlist>> GetAll()
        {
            return await Task.FromResult(_db.Wishlist);
        }

        public async Task<Wishlist> Update(Wishlist entity)
        {
            _db.Wishlist.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
