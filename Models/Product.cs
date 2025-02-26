using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ModelNo { get; set; }

    public string? Specification { get; set; }

    public int? Price { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }
}
