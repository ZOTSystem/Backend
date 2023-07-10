using System;
using System.Collections.Generic;

namespace be.Models;

public partial class Newcategory
{
    public int NewCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
