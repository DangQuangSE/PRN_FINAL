using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class MenstrualCycleHistory
{
    public int HistoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CycleLength { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Note { get; set; }

    public DateOnly? StartDate { get; set; }

    public int? CycleId { get; set; }

    public virtual MenstrualCycle? Cycle { get; set; }
}
