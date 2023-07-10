using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? SubjectId { get; set; }

    public int? AccountId { get; set; }

    public int? AnswerId { get; set; }

    public int? TypeTestId { get; set; }

    public int? LevelId { get; set; }

    public int? TopicId { get; set; }

    public string? Image { get; set; }

    public string? QuestionContext { get; set; }

    public string? OptionA { get; set; }

    public string? OptionB { get; set; }

    public string? OptionC { get; set; }

    public string? OptionD { get; set; }

    public string? Solution { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual Level? Level { get; set; }

    public virtual ICollection<Questiontest> Questiontests { get; set; } = new List<Questiontest>();

    public virtual Subject? Subject { get; set; }

    public virtual Topic? Topic { get; set; }

    public virtual Typetest? TypeTest { get; set; }
}
