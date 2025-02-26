using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string? SubName { get; set; }

    public int? CategoryId { get; set; }
}
