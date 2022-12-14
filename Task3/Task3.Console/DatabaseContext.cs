using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace Task3.Console;

public class DatabaseContext : DbContext
{
    public DbSet<XmlData> XmlData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=Task3.Console;MultipleActiveResultSets=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<XmlData>().Property(x => x.XmlString).HasColumnType("xml");
        modelBuilder.Entity<XmlData>().Property(x => x.XElement).HasColumnType("xml");


        modelBuilder.Entity<XmlData>().Property(x => x.XElement).HasConversion(
            xml => xml.ToString(),
            xml => xml != null ? XElement.Parse(xml) : null);
    }
}