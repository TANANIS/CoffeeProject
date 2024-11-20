﻿using System;
using System.Collections.Generic;

namespace BigPorject.Models;

public partial class VOrderheaderOrderdetail
{
    public string OrderId { get; set; } = null!;

    public short OrderItem { get; set; }

    public string? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Payment { get; set; }

    public string? ShipStatus { get; set; }

    public string? ProductId { get; set; }

    public short? Qty { get; set; }

    public string? Uom { get; set; }

    public short? UnitPrice { get; set; }

    public int? Totle { get; set; }

    public string? Status { get; set; }
}
