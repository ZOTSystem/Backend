using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Groupsubject
{
    public int? CombinationId { get; set; }

    public int? SubjectId { get; set; }

    public virtual Combination? Combination { get; set; }

    public virtual Subject? Subject { get; set; }
}
