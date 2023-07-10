using System;
using System.Collections.Generic;

namespace be.Models;

public partial class News
{
    public int NewId { get; set; }

    public int? NewCategoryId { get; set; }

    public int? AccountId { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Image { get; set; }

    public string? Content { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Newcategory? NewCategory { get; set; }
}
