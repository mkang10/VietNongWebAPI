using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class Guide
{
    public int GuideId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Author { get; set; }

    public DateOnly? PublishDate { get; set; }
}
