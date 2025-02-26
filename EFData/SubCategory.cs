using System;
using System.Collections.Generic;

namespace ProductManagementSystem.EFData;

public partial class SubCategory
{
    public int Id { get; set; }

    public string? SubName { get; set; }

    public int? CategoryId { get; set; }
}
