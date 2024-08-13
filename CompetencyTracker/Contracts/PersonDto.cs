using System.ComponentModel.DataAnnotations;

namespace CompetencyTracker.Contracts;

public class PersonDto
{
    public long Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2,ErrorMessage = "FFFFFFFFFFFFFFF")]
    public string Name { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 2,ErrorMessage =  "FFFFFFFFFFFFFFF")]
    public string DisplayName { get; set; }
    public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
}