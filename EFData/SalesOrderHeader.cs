using System;
using System.Collections.Generic;

namespace ProductManagementSystem.EFData;

public partial class SalesOrderHeader
{
    public int HeaderId { get; set; }

    public int? UserId { get; set; }

    public string? SalesType { get; set; }

    public DateTime? SalesDate { get; set; }

    public int? TotalAmount { get; set; }

    public int? TotalGst { get; set; }

    public int? TotalIgst { get; set; }

    public int? TotalSgst { get; set; }
}
