using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class OrderHistory
{
    public int HistoryId { get; set; }

    public int? OrderId { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly ChangeDate { get; set; }

    public virtual Order? Order { get; set; }
}
