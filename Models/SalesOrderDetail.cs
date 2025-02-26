using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class SalesOrderDetail
{
    public int SalesHeaderId { get; set; }

    public int SalesOrderDetailsId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public int? Amount { get; set; }

    public int? Gst { get; set; }

    public int? Cgst { get; set; }

    public int? Igst { get; set; }
}
