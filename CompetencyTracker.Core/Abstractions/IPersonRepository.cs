using CompetencyTracker.Core.Models;

namespace CompetencyTracker.Core.Abstractions;

public interface IPersonRepository
{
    /// <summary>
    ///     Асинхронно получает всех людей.
    /// </summary>
    /// <returns>Коллекция объектов Person.</returns>
    Task<IEnumerable<Person>> GetAllPersonAsync();

    /// <summary>
    ///     Асинхронно получает человека по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека.</param>
    /// <returns>Объект Person, если найден, иначе null.</returns>
    Task<Person> GetPersonAsync(long id);

    /// <summary>
    ///     Асинхронно создает нового человека.
    /// </summary>
    /// <param name="personModel">Модель человека для создания.</param>
    /// <returns>Созданный объект Person.</returns>
    Task<Person> CreatePersonAsync(Person personModel);

    /// <summary>
    ///     Асинхронно обновляет информацию о человеке.
    /// </summary>
    /// <param name="id">Идентификатор человека для обновления.</param>
    /// <param name="personModel">Модель обновленного человека.</param>
    /// <returns>Обновленный объект Person, если найден и обновлен, иначе null.</returns>
    Task<Person?> PutPersonAsync(long id, Person personModel);

    /// <summary>
    ///     Асинхронно удаляет человека по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека для удаления.</param>
    /// <returns>Удаленный объект Person, если найден и удален, иначе null.</returns>
    Task<Person?> DeletePerson(long id);
}