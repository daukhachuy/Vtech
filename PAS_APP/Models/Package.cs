using System;
using System.Collections.Generic;

namespace PAS_APP.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public int Due { get; set; }

    public string Detail { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
