using EfCompileSpecDemo.Models;

using Microsoft.EntityFrameworkCore;

namespace EfCompileSpecDemo.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
