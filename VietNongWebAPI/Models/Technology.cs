using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Technology
{
    public int TechId { get; set; }

    public int? FarmId { get; set; }

    public string TechName { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? ImplementationDate { get; set; }

    public virtual Farm? Farm { get; set; }
}
