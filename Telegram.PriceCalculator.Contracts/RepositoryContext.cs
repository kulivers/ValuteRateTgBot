using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Telegram.PriceCalculator.Shared;

namespace Telegram.PriceCalculator.Repository;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<RepositoryContext>()
            .UseMySQL("Server=127.0.0.1;Uid=ekul;Pwd=ekul;Database=valutebot",
                builder => builder.MigrationsAssembly("Telegram.PriceCalculator.Repository"));
        return new RepositoryContext(builder.Options);
    }
}

public class RepositoryContext : DbContext
{
    public DbSet<UserFormula> UserFormulas { get; set; }

    public RepositoryContext()
    {
        Database.EnsureCreated();
    }

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserFormula>()
               .HasKey(x => x.FormulaId); // The primary key

        builder.Entity<UserFormula>()
               .Property(x => x.FormulaId)
               .ValueGeneratedOnAdd(); // Auto-increment

        builder.Entity<UserFormula>()
               .HasMany(x => x.Variables) // One-to-many relationship
               .WithOne() // No inverse navigation
               .HasForeignKey("FormulaId") // Foreign key on Variables
               .IsRequired(); // on delete cascade by default

        builder.Entity<UserFormula>(entity =>
        {
            entity.Property(e => e.Formula).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
        });

        builder.Entity<Variable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Value).IsRequired();


        });

        builder.Entity<ValuteCalculatedVariable>(entity =>
        {
            // entity.HasKey(e => e.Id);
            // entity.Property(e => e.Name).IsRequired();
            // entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.VchCode);
        });
    }
}
