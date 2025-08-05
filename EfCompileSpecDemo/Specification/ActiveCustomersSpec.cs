

using Ardalis.Specification;

using EfCompileSpecDemo.Models;

namespace EfCompileSpecDemo.Specification;

public interface ICompiledSpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    Func<AppDbContext, IAsyncEnumerable<T>> CompiledQueryAsync { get; }
}


public class ActiveCustomersSpec : Specification<Customer> , ICompiledSpecification<Customer>
{
    public Expression<Func<Customer, bool>> Criteria { get; }
    public Func<AppDbContext, IAsyncEnumerable<Customer>> CompiledQueryAsync { get; }
    public ActiveCustomersSpec()
    {
        // Define criteria once
        Criteria = c => c.IsActive;

        CompiledQueryAsync = EF.CompileAsyncQuery((AppDbContext db) =>
             db.Set<Customer>()
               .Where(Criteria)
               .OrderBy(c => c.Name)
               .Include(c => c.Orders));
    }
}
