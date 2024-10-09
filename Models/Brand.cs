﻿using System;
using System.Collections.Generic;

namespace DoAnPhanMem.Models;

public partial class Brand
{
    public string BrandId { get; set; } = null!;

    public string? BrandName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
