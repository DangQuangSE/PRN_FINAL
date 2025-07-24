using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StisService
{
    public int ServiceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public int? Discount { get; set; }

    public string? Duration { get; set; }

    public int? MaxBookingsPerSlot { get; set; }

    public decimal? Price { get; set; }

    public string? ServiceName { get; set; }

    public string? Status { get; set; }

    public string? Tests { get; set; }

    public string? Type { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<StisBooking> StisBookings { get; set; } = new List<StisBooking>();

    public virtual ICollection<StisFeedback> StisFeedbacks { get; set; } = new List<StisFeedback>();
}
