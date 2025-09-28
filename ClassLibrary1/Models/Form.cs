using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Form
{
    public string FormId { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime? Due { get; set; }

    public string? Info { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
