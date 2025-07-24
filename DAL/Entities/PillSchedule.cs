using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class PillSchedule
{
    public int ScheduleId { get; set; }

    public string ConfirmToken { get; set; } = null!;

    public bool? HasTaken { get; set; }

    public bool? IsPlacebo { get; set; }

    public DateOnly? PillDate { get; set; }

    public int? PillId { get; set; }

    public virtual Pill? Pill { get; set; }
}
