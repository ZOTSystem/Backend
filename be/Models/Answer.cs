using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Answer
{
    public int AnswerId { get; set; }

    public string? AnswerName { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<Questiontest> Questiontests { get; set; } = new List<Questiontest>();
}
