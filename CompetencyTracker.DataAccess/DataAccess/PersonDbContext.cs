using CompetencyTracker.Configurations;
using CompetencyTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.DataAccess;

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