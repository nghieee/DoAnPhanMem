using System;
using System.Collections.Generic;

namespace DoAnPhanMem.Models;

public partial class ListProductImg
{
    public int Id { get; set; }

    public string? ProId { get; set; }

    public string? ImgUrl { get; set; }

    public virtual Product? Pro { get; set; }
}
