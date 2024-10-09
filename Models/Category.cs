using System;
using System.Collections.Generic;

namespace DoAnPhanMem.Models;

public partial class Category
{
    public string CateId { get; set; } = null!;

    public string? CateName { get; set; }

    public string? CateImg { get; set; }

    public int? CateProductCount { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
