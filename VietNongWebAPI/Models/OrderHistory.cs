﻿using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models;

public partial class OrderHistory
{
    public int OrderHistoryId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? OrderId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
