using CompetencyTracker.Contracts;
using CompetencyTracker.Models;

namespace CompetencyTracker.Services.Interfaces;

public interface IPersonService
{
    /// <summary>
    /// Возвращает список всех людей.
    /// </summary>
    /// <returns>Список объектов PersonDto.</returns>
    Task<IEnumerable<PersonDto>> GetPersons();
    
    /// <summary>
    /// Возвращает информацию о человеке по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека.</param>
    /// <returns>Объект PersonDto, если найден, иначе null.</returns>
    Task<PersonDto?> GetPerson(long id);
    
    /// <summary>
    /// Создает нового человека.
    /// </summary>
    /// <param name="createPersonDto">Данные для создания нового человека.</param>
    /// <returns>Созданный объект PersonDto.</returns>
    Task<PersonDto> PostPerson(PersonDto createPersonDto);
    
    /// <summary>
    /// Обновляет информацию о человеке по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека, которого нужно обновить.</param>
    /// <param name="updatePersonDto">Данные для обновления.</param>
    /// <returns>Обновленный объект PersonDto.</returns>
    Task<PersonDto> PutPerson(long id, PersonDto updatePersonDto);
    
    /// <summary>
    /// Удаляет человека по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор человека, которого нужно удалить.</param>
    /// <returns>Удаленный объект Person или null, если человек не найден.</returns>
    Task<Person?> DeletePerson(long id);

}