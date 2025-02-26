using System;
using System.Collections.Generic;

namespace ProductManagementSystem.EFData;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int? Userid { get; set; }

    public string? ProductId { get; set; }
}
