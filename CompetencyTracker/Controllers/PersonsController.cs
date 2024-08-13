using CompetencyTracker.Contracts;
using CompetencyTracker.DataAccess;
using CompetencyTracker.Extensions;
using CompetencyTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonsController : Controller
{
    private readonly PersonDbContext _context;

    public PersonsController(PersonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        var persons = await _context.Persons.Include(p => p.Skills).ToListAsync();
        return persons.Select(p => p.ToDto()).ToList();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(long id)
    {
        var person = await _context.Persons.Include(p => p.Skills).FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return person.ToDto();

    }
    [HttpPost]
    public async Task<ActionResult<PersonDto>> PostPerson(PersonDto createPersonDto)
    {
        var person = new Person
        {
            Name = createPersonDto.Name,
            DisplayName = createPersonDto.DisplayName,
            Skills = createPersonDto.Skills.Select(s => new Skill
            {
                Name = s.Name,
                Level = s.Level
            }).ToList()
        };

        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

      

        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person.ToDto());
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(long id, PersonDto updatePersonDto)
    {
        var person = await _context.Persons.Include(p => p.Skills).FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        person.Name = updatePersonDto.Name;
        person.DisplayName = updatePersonDto.DisplayName;
        person.Skills = updatePersonDto.Skills.Select(s => new Skill
        {
            Name = s.Name,
            Level = s.Level
        }).ToList();

        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }
   
}

   

   
    
    
    
    

