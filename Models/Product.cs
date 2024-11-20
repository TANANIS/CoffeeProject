using System;
using System.Collections.Generic;

namespace BigPorject.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public short? Price { get; set; }

    public string? Category { get; set; }

    public string? Uom { get; set; }

    public string? Description { get; set; }

    public string? Country { get; set; }

    public string? Baking { get; set; }

    public string? Flavor { get; set; }

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? Timelimit { get; set; }

    public byte? Fragrance { get; set; }

    public byte? Sour { get; set; }

    public byte? Bitter { get; set; }

    public byte? Sweet { get; set; }

    public byte? Strong { get; set; }

    public string? Method { get; set; }

    public string? ImgA { get; set; }

    public string? ImgB { get; set; }

    public string? ImgC { get; set; }

    public string? ImgD { get; set; }

    public short? Weight { get; set; }

    public string? Status { get; set; }
}
