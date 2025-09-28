using System;
using System.Collections.Generic;

namespace PAS_APP.Models;

public partial class Form
{
    public string FormId { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime Due { get; set; }

    public string? Infor { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
