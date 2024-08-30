using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Weather
{
    public int WeatherId { get; set; }

    public string Location { get; set; } = null!;

    public DateOnly Date { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? Humidity { get; set; }

    public string? Forecast { get; set; }
}
