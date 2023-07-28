using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Postlike
{
    public int PostLikeId { get; set; }

    public int? AccountId { get; set; }

    public int PostId { get; set; }

    public DateTime? LikeDate { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Post? Post { get; set; }
}
