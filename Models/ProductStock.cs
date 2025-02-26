using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class ProductStock
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? StockIn { get; set; }

    public int? StockOut { get; set; }

    public int? CurrentStock { get; set; }
}
