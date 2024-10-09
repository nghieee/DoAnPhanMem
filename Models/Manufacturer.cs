using System;
using System.Collections.Generic;

namespace DoAnPhanMem.Models;

public partial class Manufacturer
{
    public string ManId { get; set; } = null!;

    public string? ManName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
