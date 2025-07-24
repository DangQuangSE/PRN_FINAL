using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ConsultantProfile
{
    public int ProfileId { get; set; }

    public bool? EmploymentStatus { get; set; }

    public int ExperienceYears { get; set; }

    public double HourlyRate { get; set; }

    public string? Introduction { get; set; }

    public bool? IsAvailable { get; set; }

    public string? JobTitle { get; set; }

    public string? Languages { get; set; }

    public string? Location { get; set; }

    public double? Rating { get; set; }

    public int? ReviewCount { get; set; }

    public string? Specialization { get; set; }

    public int? ConsultantId { get; set; }

    public virtual User? Consultant { get; set; }

    public virtual ICollection<ConsultantFeedback> ConsultantFeedbacks { get; set; } = new List<ConsultantFeedback>();

    public virtual ICollection<ProfileDetail> ProfileDetails { get; set; } = new List<ProfileDetail>();
}
