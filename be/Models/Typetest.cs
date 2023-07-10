using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Typetest
{
    public int TypeTestId { get; set; }

    public string? TestName { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
