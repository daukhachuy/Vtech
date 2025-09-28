using System;
using System.Collections.Generic;

namespace PAS_APP.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public DateTime? Dob { get; set; }

    public string Email { get; set; } = null!;

    public string? PassWord { get; set; }

    public string? Phone { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyAddress { get; set; }

    public string? CompanyCode { get; set; }

    public DateTime CreateAt { get; set; }

    public bool Status { get; set; }

    public int? PackageId { get; set; }

    public string? FormId { get; set; }

    public string? FilterId { get; set; }

    public DateTime? DateBuyPackage { get; set; }

    public virtual Filter? Filter { get; set; }

    public virtual Form? Form { get; set; }

    public virtual Notification? Notification { get; set; }

    public virtual Package? Package { get; set; }
}
