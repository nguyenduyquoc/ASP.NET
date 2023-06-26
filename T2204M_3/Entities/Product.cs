﻿using System;
using System.Collections.Generic;

namespace T2204M_3.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public decimal Price { get; set; }

    public int Qty { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
