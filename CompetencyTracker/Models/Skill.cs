﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CompetencyTracker.Models;

public class Skill
{
    public long Id { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
    public long PersonId { get; set; } 
    public Person Person { get; set; }
}