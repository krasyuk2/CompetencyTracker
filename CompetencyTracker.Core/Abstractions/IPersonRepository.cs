using CompetencyTracker.Models;

namespace CompetencyTracker.Core.Abstractions;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllPersonAsync();
    Task<Person> GetPersonAsync(long id);
    Task<Person> CreatePersonAsync(Person personModel);
    Task<Person?> PutPersonAsync(long id, Person personModel);
    Task<Person?> DeletePerson(long id);
}