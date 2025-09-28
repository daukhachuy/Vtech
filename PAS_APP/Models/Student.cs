using System;
using System.Collections.Generic;

namespace PAS_APP.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int BirthYear { get; set; }

    public string? Ethnicity { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PermanentAddress { get; set; }

    public string? CurrentAddress { get; set; }

    public decimal? GpaThpt { get; set; }

    public string? Ielts { get; set; }

    public string? Toeic { get; set; }

    public string? OtherCertificates { get; set; }

    public string? SelfIntroduction { get; set; }

    public string? FatherName { get; set; }

    public int? FatherBirthYear { get; set; }

    public string? FatherPhone { get; set; }

    public string? FatherOccupation { get; set; }

    public string? FatherCurrentAddress { get; set; }

    public string? MotherName { get; set; }

    public int? MotherBirthYear { get; set; }

    public string? MotherPhone { get; set; }

    public string? MotherOccupation { get; set; }

    public string? MotherCurrentAddress { get; set; }

    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
