using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Combination
{
    public int CombinationId { get; set; }

    public int? SchoolId { get; set; }

    public string? CombinationName { get; set; }

    public string? MajorName { get; set; }

    public double? Score { get; set; }

    public virtual School? School { get; set; }
}
