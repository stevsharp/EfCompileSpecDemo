using EfCompileSpecDemo.Data;
using EfCompileSpecDemo.Models;

using Microsoft.EntityFrameworkCore;

namespace EfCompileSpecDemo.Queries;

public static class CompiledQueries
{
    public static readonly Func<AppDbContext, bool, IEnumerable<Customer>> ActiveCustomers =
        EF.CompileQuery((AppDbContext db, bool isActive) =>
            db.Customers
              .Where(c => c.IsActive == isActive)
              .OrderBy(c => c.Name)
              .Include(c => c.Orders));

    public static readonly Func<AppDbContext, bool, IAsyncEnumerable<Customer>> ActiveCustomersAsync =
    EF.CompileAsyncQuery((AppDbContext db, bool isActive) =>
        db.Customers
          .Where(c => c.IsActive == isActive)
          .OrderBy(c => c.Name)
          .Include(c => c.Orders));
}
