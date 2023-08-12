using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string? TopicName { get; set; }

    public string? Duration { get; set; }

    public int? TotalQuestion { get; set; }

    public int? TopicType { get; set; }

    public int? Grade { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public int? SubjectId { get; set; }

    public DateTime? StartTestDate { get; set; }

    public DateTime? FinishTestDate { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Subject? Subject { get; set; }
}
