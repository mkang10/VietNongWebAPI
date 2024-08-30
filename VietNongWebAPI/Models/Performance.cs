using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Performance
{
    public int PerformanceId { get; set; }

    public int? FarmId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Yield { get; set; }

    public string? Notes { get; set; }

    public virtual Farm? Farm { get; set; }
}
