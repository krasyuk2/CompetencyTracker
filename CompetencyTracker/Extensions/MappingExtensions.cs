using CompetencyTracker.Contracts;
using CompetencyTracker.Models;

namespace CompetencyTracker.Extensions;

public static class MappingExtensions
{
    public static PersonDto ToDto(this Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            DisplayName = person.DisplayName,
            Skills = person.Skills.Select(s => s.ToDto()).ToList()
        };
    }
    public static SkillDto ToDto(this Skill skill)
    {
        return new SkillDto
        {
            Name = skill.Name,
            Level = skill.Level
        };
    }

}