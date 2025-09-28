using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Filter
{
    public string FilterId { get; set; } = null!;

    public int ServiceId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateAt { get; set; }

    public string FileName { get; set; } = null!;

    public string? Note { get; set; }

    public int NumberOfRecord { get; set; }

    public int PassRate { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
