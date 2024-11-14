using System;
using System.Collections.Generic;

namespace WebApplication1_1105_TSET_member5.Models;

public partial class Cartdetail
{
    public string CartId { get; set; } = null!;

    public short CartItemId { get; set; }

    public string? ProductId { get; set; }

    public short? Qty { get; set; }

    public short? UnitPrice { get; set; }

    public int? TotalPrice { get; set; }

    public DateTime? CreateDate { get; set; }
}
