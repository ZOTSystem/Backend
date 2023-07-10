using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Postfavourite
{
    public int PostFavouriteId { get; set; }

    public int? AccountId { get; set; }

    public int? PostId { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Post? Post { get; set; }
}
