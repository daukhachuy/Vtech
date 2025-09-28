using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Detail { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual ICollection<Filter> Filters { get; set; } = new List<Filter>();
}
