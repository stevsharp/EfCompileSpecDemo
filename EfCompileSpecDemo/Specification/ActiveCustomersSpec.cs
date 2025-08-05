

using Ardalis.Specification;

using EfCompileSpecDemo.Models;

namespace EfCompileSpecDemo.Specification;

public class ActiveCustomersSpec : Specification<Customer>
{
    public ActiveCustomersSpec()
    {
        Query.Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Include(x => x.Orders);
    }
}
