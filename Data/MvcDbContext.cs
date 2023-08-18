using CRUDMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRUDMVC.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions<MvcDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
