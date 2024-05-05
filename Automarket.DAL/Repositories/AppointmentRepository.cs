using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.DAL.Repositories
{
    public class AppointmentRepository : IBaseRepository<Appointment>
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Appointment entity)
        {
            await _db.Appointments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Appointment entity)
        {
            _db.Appointments.Remove(entity);
            //await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<Appointment>> GetAll()
        {
            return await Task.FromResult(_db.Appointments);
        }

        public async Task<Appointment> Update(Appointment entity)
        {
            _db.Appointments.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
