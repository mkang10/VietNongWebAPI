using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Farm
{
    public int FarmId { get; set; }

    public int? UserId { get; set; }

    public string FarmName { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? Size { get; set; }

    public virtual ICollection<Crop> Crops { get; set; } = new List<Crop>();

    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    public virtual ICollection<Technology> Technologies { get; set; } = new List<Technology>();

    public virtual User? User { get; set; }
}
