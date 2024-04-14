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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(User entity)
        {
            await _db.User.AddAsync(entity);
            //await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<User>> GetAll()
        {
            return await Task.FromResult(_db.User);
        }

        public async Task Delete(User entity)
        {
            _db.User.Remove(entity);
            //await _db.SaveChangesAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.User.Update(entity);
            //await _db.SaveChangesAsync();

            return entity;
        }
    }
}
