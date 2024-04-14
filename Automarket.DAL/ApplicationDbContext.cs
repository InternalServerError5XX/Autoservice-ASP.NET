using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Automarket.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Username = "admin",
                        Email = "secauto.admin@gmail.com",
                        Name = "Vlad",
                        Lastname = "Linnik",
                        Password = HashPasswordHelper.HashPassword("adminvlad"),
                        Age = 20,
                        Role = Role.Admin,
                        CreationDate = DateTime.Now,
                        LastLogin = DateTime.Now
                    },
                    new User()
                    {
                        Id = 2,
                        Username = "administrator",
                        Email = "secauto.administrator@gmail.com",
                        Name = "Dimasik",
                        Lastname = "Hranoskyi",
                        Password = HashPasswordHelper.HashPassword("admindima"),
                        Age = 20,
                        Role = Role.Administrator,
                        CreationDate = DateTime.Now,
                        LastLogin = DateTime.Now
                    },
                    new User()
                    {
                        Id = 3,
                        Username = "mechanic",
                        Email = "secauto.mechanic@gmail.com",
                        Name = "Andriy",
                        Lastname = "Ishchuk",
                        Password = HashPasswordHelper.HashPassword("mechanicandriy"),
                        Age = 20,
                        Role = Role.Mechanic,
                        CreationDate = DateTime.Now,
                        LastLogin = DateTime.Now
                    },
                    new User()
                    {
                        Id = 4,
                        Username = "TestUser",
                        Email = "secauto.testuser@gmail.com",
                        Name = "Test",
                        Lastname = "User",
                        Password = HashPasswordHelper.HashPassword("testuser"),
                        Age = 20,
                        Role = Role.User,
                        CreationDate = DateTime.Now,
                        LastLogin = DateTime.Now
                    },
                });
            });
        }
    }
}
