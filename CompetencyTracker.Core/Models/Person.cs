﻿
namespace CompetencyTracker.Models;

public class Person
{
 
    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Skill> Skills { get; set; } = new List<Skill>();

}