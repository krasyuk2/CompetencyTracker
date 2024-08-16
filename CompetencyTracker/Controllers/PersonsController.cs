using CompetencyTracker.Application.Contracts;
using CompetencyTracker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompetencyTracker.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonsController(IPersonService personService, ILogger<PersonsController> logger) : Controller
{
    private readonly ILogger<PersonsController> _logger = logger;
    private readonly IPersonService _personService = personService;

    /// <summary>
    ///     Возвращает список всех людей.
    /// </summary>
    /// <returns>Список объектов PersonDto.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        var persons = await _personService.GetPersons();
        return Ok(persons);
    }

    /// <summary>
    ///     Возвращает информацию о человеке по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека.</param>
    /// <returns>Объект PersonDto, если найден, иначе NotFound.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    ///     Создает нового человека.
    /// </summary>
    /// <param name="createPersonDto">Данные для создания нового человека.</param>
    /// <returns>Созданный объект PersonDto.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    ///     Обновляет информацию о человеке по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека, которого нужно обновить.</param>
    /// <param name="updatePersonDto">Данные для обновления.</param>
    /// <returns>Сообщение об успешном обновлении или NotFound, если человек не найден.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutPerson(long id, PersonDto updatePersonDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var person = await _personService.PutPerson(id, updatePersonDto);

        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }

        return Ok("Person updated");
    }

    /// <summary>
    ///     Удаляет человека по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека, которого нужно удалить.</param>
    /// <returns>Сообщение об успешном удалении или NotFound, если человек не найден.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var person = await _personService.DeletePerson(id);
        if (person == null)
        {
            _logger.LogError($"Person whit id {id} not found");
            return NotFound();
        }

        return Ok("Person deleted");
    }
}