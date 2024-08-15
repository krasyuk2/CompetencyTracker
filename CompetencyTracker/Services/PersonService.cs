using System.Text.Json;
using CompetencyTracker.Contracts;
using CompetencyTracker.DataAccess;
using CompetencyTracker.Extensions;
using CompetencyTracker.Models;
using CompetencyTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.Services;

public class PersonService : IPersonService
{
    private PersonDbContext _context;
    public PersonService(PersonDbContext context) => _context = context;
    
    public async Task<IEnumerable<PersonDto>> GetPersons()
    {
        var persons = await _context.Persons.Include(p => p.Skills).ToListAsync();
        return persons.Select(p => p.ToDto()).ToList();
    }

    public async Task<PersonDto?> GetPerson(long id)
    {
        var person = await _context.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        return person?.ToDto();
    }

    public async Task<PersonDto> PostPerson([FromBody] PersonDto createPersonDto)
    {
        foreach (var skill in createPersonDto.Skills)
        {
            if (skill.Level < 1 || skill.Level > 10)
            {
                throw new ArgumentException($"Skill level must be between 1 and 10. Skill: {skill.Name}, Level: {skill.Level}");
            }
        }
        
        var person = new Person
        {
            Name = createPersonDto.Name,
            DisplayName = createPersonDto.DisplayName,
            
            Skills = createPersonDto.Skills.Select(s => new Skill
            {
                Name = s.Name,
                Level = (byte)s.Level
            }).ToList()
        };
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();
        return person.ToDto();
    }

    public async Task<PersonDto> PutPerson(long id, PersonDto updatePersonDto)
    {
        var person = await _context.Persons.Include(p => p.Skills).FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
            return null;
        
        person.Name = updatePersonDto.Name;
        person.DisplayName = updatePersonDto.DisplayName;
        person.Skills = updatePersonDto.Skills.Select(s => new Skill
        {
            Name = s.Name,
            Level = (byte)s.Level
        }).ToList();

        await _context.SaveChangesAsync();
        return person.ToDto();
    }

    public async Task<Person> DeletePerson(long id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person != null)
        {
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
        }
        return person;
    }
}