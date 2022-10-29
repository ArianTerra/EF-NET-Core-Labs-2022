using Microsoft.EntityFrameworkCore;
using Task1.DAL.DomainModels.TPH;
using Task1.DAL.DomainModels.TPT;

namespace Task1.DAL;

public class DatabaseContext : DbContext
{
    // TPH example
    public DbSet<Material> Materials { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Article> Articles { get; set; }

    // TPT example
    public DbSet<UniversityUser> UniversityUsers { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPT configuration
        modelBuilder.Entity<Student>().ToTable("Students");
        modelBuilder.Entity<Teacher>().ToTable("Teachers");
    }
}