using AT_Management.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace AT_Management.Data
{
    public class ATDbContext : IdentityDbContext<ApplicationUser>
    {
        public ATDbContext(DbContextOptions<ATDbContext> options) : base(options)
        {
            
        }
        

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Form> Form { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var employeeRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = employeeRoleId,
                    ConcurrencyStamp = employeeRoleId,
                    Name = "Employee",
                    NormalizedName = "Employee".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}



