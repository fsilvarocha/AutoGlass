using AutoGlass.Core.Domain.Entities;
using AutoGlass.Core.Domain.Notification;
using Microsoft.EntityFrameworkCore;

namespace AutoGlass.Infra.Data.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Notification>()
            .HasNoKey();


        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
}
