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
    private ILogger<PersonsController> _logger;

    public PersonsController(PersonDbContext context, ILogger<PersonsController> logger)
    {
        _context = context;
        _logger = logger;
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
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }

        return person.ToDto();

    }
    [HttpPost]
    public async Task<ActionResult<PersonDto>> PostPerson(PersonDto createPersonDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Model validation errors: {ModelState}");
            return BadRequest();
        }
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
        try
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding person");
            return StatusCode(500);
        }
       

      

        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person.ToDto());
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(long id, PersonDto updatePersonDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var person = await _context.Persons.Include(p => p.Skills).FirstOrDefaultAsync(p => p.Id == id);

        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
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
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }
   
}

   

   
    
    
    
    

