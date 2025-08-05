

namespace EfCompileSpecDemo.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool IsActive { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}
