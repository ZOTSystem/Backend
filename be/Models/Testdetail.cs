using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Testdetail
{
    public int TestDetailId { get; set; }

    public int? AccountId { get; set; }

    public double? Score { get; set; }

    public bool? Submitted { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Questiontest> Questiontests { get; set; } = new List<Questiontest>();
}
