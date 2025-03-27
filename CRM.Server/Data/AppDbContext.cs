using CRM.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }


    }
}
