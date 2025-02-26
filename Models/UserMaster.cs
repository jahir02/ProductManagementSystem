using System;
using System.Collections.Generic;

namespace ProductManagementSystem.Models;

public partial class UserMaster
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
}
