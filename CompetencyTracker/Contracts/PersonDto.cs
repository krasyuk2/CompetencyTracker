﻿namespace CompetencyTracker.Contracts;

public class PersonDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<SkillDto> Skills { get; set; } = new List<SkillDto>();
}