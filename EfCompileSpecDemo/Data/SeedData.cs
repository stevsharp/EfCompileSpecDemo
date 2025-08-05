

namespace EfCompileSpecDemo.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext db)
    {
        if (db.Customers.Any())
            return;

        var customers = Enumerable.Range(1, 10).Select(i => new Models.Customer
        {
            Id = i,
            Name = $"Customer {i}",
            IsActive = i % 2 == 0,
            Orders = [.. Enumerable.Range(1, 3).Select(j => new Models.Order
            {
                Product = $"Product {j}",
                Price = j * 10
            })]
        });

        db.Customers.AddRange(customers);
        db.SaveChanges();
    }

}
