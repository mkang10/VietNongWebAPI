using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Crop
{
    public int CropId { get; set; }

    public int? FarmId { get; set; }

    public string CropName { get; set; } = null!;

    public DateOnly? PlantingDate { get; set; }

    public DateOnly? HarvestDate { get; set; }

    public virtual Farm? Farm { get; set; }
}
