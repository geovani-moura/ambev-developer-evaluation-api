using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValueSql("'V-' || EXTRACT(YEAR FROM CURRENT_DATE)::TEXT || TO_CHAR(CURRENT_DATE, 'MM') || '-' || LEFT(gen_random_uuid()::TEXT, 8)");

        builder.HasIndex(s => s.SaleNumber)
            .IsUnique();

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.CustomerId)
            .IsRequired();

        builder.Property(s => s.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.CustomerEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.CustomerPhone)
            .HasMaxLength(20);

        builder.Property(s => s.Branch)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .HasColumnName("Cancelled")
            .HasDefaultValue(false);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(i => i.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
