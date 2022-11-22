using Microsoft.EntityFrameworkCore;

namespace Task2.Console;

public class DatabaseContext : DbContext
{
    public DbSet<MedievalVassal> Vassals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=Task2.Console;MultipleActiveResultSets=True",
            x => x.UseHierarchyId());
    }
}