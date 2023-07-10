using System;
using System.Collections.Generic;

namespace be.Models;

public partial class School
{
    public int SchoolId { get; set; }

    public string? SchoolName { get; set; }

    public virtual ICollection<Combination> Combinations { get; set; } = new List<Combination>();
}
