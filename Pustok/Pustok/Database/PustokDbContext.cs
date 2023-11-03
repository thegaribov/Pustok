using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pustok.Contracts;
using Pustok.Database.DomainModels;

namespace Pustok.Database;

public class PustokDbContext : DbContext
{
    public PustokDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions) { }



    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
