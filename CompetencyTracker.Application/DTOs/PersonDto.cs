using System.ComponentModel.DataAnnotations;

namespace CompetencyTracker.Application.Contracts;

public class PersonDto
{
    public long Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string DisplayName { get; set; }

    public List<SkillDto> Skills { get; set; } = new();
}