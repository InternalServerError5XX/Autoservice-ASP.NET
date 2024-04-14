using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;

namespace Automarket
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Consumable>, ConsumableRepository>();
            services.AddScoped<IBaseRepository<Appointment>, AppointmentRepository>();
            services.AddScoped<IBaseRepository<Maintenance>, MaintenanceRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IConsumableService, ConsumableService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
        }
    }
}
