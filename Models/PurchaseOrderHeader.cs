using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class PurchaseOrderHeader
{
    public int HeaderId { get; set; }

    public int? UserId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? TotalAmount { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; } = new List<PurchaseOrderDetail>();
}
