using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MenstrualCycle
{
    public int CycleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CycleLength { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? LastNotificationDate { get; set; }

    public string? LastNotificationType { get; set; }

    public string? Note { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? CustomerId { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<MenstrualCycleHistory> MenstrualCycleHistories { get; set; } = new List<MenstrualCycleHistory>();
}
