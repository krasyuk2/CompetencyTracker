using System.ComponentModel.DataAnnotations;

namespace CompetencyTracker.Application.Contracts;

public class SkillDto
{
    [Required] public string Name { get; set; }

    [Range(1, 10, ErrorMessage = "Level must be between 1 and 10")]
    public int Level { get; set; }
}