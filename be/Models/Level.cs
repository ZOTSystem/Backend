using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Level
{
    public int LevelId { get; set; }

    public string? LevelName { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
