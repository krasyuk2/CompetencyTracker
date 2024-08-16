using CompetencyTracker.Contracts;
using CompetencyTracker.Core.Abstractions;
using CompetencyTracker.Extensions;
using CompetencyTracker.Models;
using CompetencyTracker.Services.Interfaces;

namespace CompetencyTracker.Services;

public class PersonService : IPersonService
{
    private IPersonRepository _personRepository;
    public PersonService(IPersonRepository personRepository) => _personRepository = personRepository;

    public async Task<IEnumerable<PersonDto>> GetPersons()
    {
        var persons = await _personRepository.GetAllPersonAsync();
        return persons.Select(p => p.ToDto()).ToList();
    }

    public async Task<PersonDto?> GetPerson(long id)
    {
        var person = await _personRepository.GetPersonAsync(id);
        if (person == null) return null;
        return person.ToDto();
    }

    public async Task<PersonDto> PostPerson(PersonDto createPersonDto)
    {
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
        var personResponse = await _personRepository.CreatePersonAsync(person);
        return personResponse.ToDto();
    }

    public async Task<PersonDto> PutPerson(long id, PersonDto personDto)
    {
        var person = new Person
        {
            Name = personDto.Name,
            DisplayName = personDto.DisplayName,
            Skills = personDto.Skills.Select(s => new Skill
            {
                Name = s.Name,
                Level = (byte)s.Level
            }).ToList()
        };
        var personResponse = await _personRepository.PutPersonAsync(id, person);
        if (personResponse == null) return null;
        return personResponse.ToDto();
    }
    
    public async Task<Person?> DeletePerson(long id)
    {
       var person = await _personRepository.DeletePerson(id);
       if (person == null) return null;
       return person;
    }
}
  