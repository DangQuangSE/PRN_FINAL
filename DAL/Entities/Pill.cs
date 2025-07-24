using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Pill
{
    public int PillId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? NotificationFrequency { get; set; }

    public string? PillType { get; set; }

    public DateOnly? StartDate { get; set; }

    public TimeOnly? TimeOfDay { get; set; }

    public int? CustomerId { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<PillSchedule> PillSchedules { get; set; } = new List<PillSchedule>();
}
