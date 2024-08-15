using CompetencyTracker.Contracts;
using CompetencyTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompetencyTracker.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonsController(IPersonService personService, ILogger<PersonsController> logger) : Controller
{
    
    private readonly ILogger<PersonsController> _logger = logger;
    private readonly IPersonService _personService = personService;
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        var persons= await _personService.GetPersons();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetPerson(long id)
    {
        var person = await _personService.GetPerson(id);

        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult> PostPerson(PersonDto createPersonDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Model validation errors: {ModelState}");
            return BadRequest();
        }
        var person = await _personService.PostPerson(createPersonDto);
        return Ok(person);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(long id, PersonDto updatePersonDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var person = await _personService.PutPerson(id, updatePersonDto);

        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var person = await _personService.DeletePerson(id);
        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }
        return Ok();
    }
}