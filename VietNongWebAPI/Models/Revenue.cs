using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Revenue
{
    public int RevenueId { get; set; }

    public int? FarmerId { get; set; }

    public decimal? TotalRevenue { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? Farmer { get; set; }
}
