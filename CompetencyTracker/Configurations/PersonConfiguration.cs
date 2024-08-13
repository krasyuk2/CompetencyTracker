using CompetencyTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompetencyTracker.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);
        builder
            .HasMany(p => p.Skills)
            .WithOne(s => s.Person);
    }
}