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
    public class CarRepository : IBaseRepository<Car>
    {
        private readonly ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Car entity)
        {
            await _db.Car.AddAsync(entity);
            //await _db.SaveChangesAsync();
        }

        public IQueryable<Car> GetAll()
        {
            return _db.Car;
        }

        public async Task Delete(Car entity)
        {
            _db.Car.Remove(entity);
            //await _db.SaveChangesAsync();
        }

        public async Task<Car> Update(Car entity)
        {
            _db.Car.Update(entity);
            //await _db.SaveChangesAsync();

            return entity;
        }
    }
}
