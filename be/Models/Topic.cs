using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string? Duration { get; set; }

    public int? TotalQuestion { get; set; }

    public string? TopicName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
