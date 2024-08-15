using CompetencyTracker.Contracts;
using CompetencyTracker.Models;

namespace CompetencyTracker.Services.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonDto>> GetPersons();
    Task<PersonDto?> GetPerson(long id); 
    Task<PersonDto> PostPerson(PersonDto createPersonDto);
    Task<PersonDto> PutPerson(long id, PersonDto updatePersonDto);
    Task<Person> DeletePerson(long id);

}