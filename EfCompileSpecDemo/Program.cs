using EfCompileSpecDemo.Data;
using EfCompileSpecDemo.Models;
using EfCompileSpecDemo.Repositories;
using EfCompileSpecDemo.Specification;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite("Data Source=specdemo.db")
    .Options;

using (var db = new AppDbContext(options))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}

Console.WriteLine("Running benchmarks...");

var stopwatch = new Stopwatch();
const int iterations = 500;

using (var db = new AppDbContext(options))
{
    var repo = new EfRepository<Customer>(db);
    var spec = new ActiveCustomersSpec();

    //  Normal spec execution
    stopwatch.Start();
    for (int i = 0; i < iterations; i++)
        await repo.ListAsync(spec);
    stopwatch.Stop();
    Console.WriteLine($"Normal Specification: {stopwatch.ElapsedMilliseconds} ms");

    // Compiled query execution
    stopwatch.Restart();
    for (int i = 0; i < iterations; i++)
    {
        await repo.ListAsyncWithCompileQuery(spec);
    }
    stopwatch.Stop();
    Console.WriteLine($"Compiled Query:       {stopwatch.ElapsedMilliseconds} ms");
}

Console.WriteLine("Done");

Console.ReadLine();