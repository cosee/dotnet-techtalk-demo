using CandyBackend.Repository.Candies;
using CandyBackend.Repository.Orders;
using Microsoft.EntityFrameworkCore;

namespace CandyBackend.Repository;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("timestamp without time zone");
            entity.HasMany<OrderItemEntity>(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();
        });
    }

    public required DbSet<CandyEntity> Candy { get; init; }
    public required DbSet<OrderEntity> Order { get; init; }
}