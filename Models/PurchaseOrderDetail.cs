using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class PurchaseOrderDetail
{
    public int PurchaseHeaderId { get; set; }

    public int PurchaseOrderDetailsId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public int? Amount { get; set; }

    public virtual PurchaseOrderHeader PurchaseHeader { get; set; } = null!;
}
