using CompetencyTracker.Core.Abstractions;
using CompetencyTracker.Core.Models;
using CompetencyTracker.DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.DataAccess.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonDbContext _context;

    public PersonRepository(PersonDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetAllPersonAsync()
    {
        var persons = await _context.Persons
            .Include(p => p.Skills)
            .ToListAsync();
        return persons;
    }

    public async Task<Person> GetPersonAsync(long id)
    {
        var person = await _context.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        return person;
    }

    public async Task<Person> CreatePersonAsync(Person model)
    {
        await _context.Persons.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Person?> PutPersonAsync(long id, Person model)
    {
        var person = await _context.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (person == null) return null;

        person.Name = model.Name;
        person.DisplayName = model.DisplayName;
        person.Skills = model.Skills.Select(s => new Skill
        {
            Name = s.Name,
            Level = s.Level
        }).ToList();

        await _context.SaveChangesAsync();
        return person;
    }

    public async Task<Person?> DeletePerson(long id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null) return null;

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return person;
    }
}