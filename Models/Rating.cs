﻿using System;
using System.Collections.Generic;

namespace DoAnPhanMem.Models;

public partial class Rating
{
    public string RateId { get; set; } = null!;

    public double? RateNumber { get; set; }

    public DateTime? DayClose { get; set; }

    public int? PercentSale { get; set; }

    public string? ProId { get; set; }

    public string? UserId { get; set; }

    public virtual Product? Pro { get; set; }

    public virtual User? User { get; set; }
}
