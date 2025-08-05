
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

using EfCompileSpecDemo.Data;
using EfCompileSpecDemo.Models;
using EfCompileSpecDemo.Queries;
using EfCompileSpecDemo.Specification;

using Microsoft.EntityFrameworkCore;

namespace EfCompileSpecDemo.Repositories;

public class EfRepository<T>(AppDbContext dbContext) where T : class
{

    private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        var evaluator = new SpecificationEvaluator();
        var query = evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsyncWithCompileQuery(ISpecification<T> spec)
    {

        if (typeof(T) == typeof(Customer) && spec is ActiveCustomersSpec)
        {
            var result = CompiledQueries.ActiveCustomers(_dbContext, true).ToList();
            return (IReadOnlyList<T>)result;
        }

        // Otherwise use the normal spec evaluation
        var evaluator = new SpecificationEvaluator();
        var query = evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        return await query.ToListAsync();
    }
}
