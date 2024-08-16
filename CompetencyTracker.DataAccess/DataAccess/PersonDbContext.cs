using CompetencyTracker.Core.Models;
using CompetencyTracker.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.DataAccess.DataAccess;

public class PersonDbContext(DbContextOptions<PersonDbContext> options)
    : DbContext(options)
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new SkillConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}