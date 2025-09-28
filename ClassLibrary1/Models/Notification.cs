using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Notification
{
    public int UserId { get; set; }

    public string? Title { get; set; }

    public string? Detail { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual User User { get; set; } = null!;
}
