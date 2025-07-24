using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StisFeedback
{
    public int FeedbackId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Rating { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserId { get; set; }

    public int? BookingId { get; set; }

    public int? ServiceId { get; set; }

    public virtual StisBooking? Booking { get; set; }

    public virtual StisService? Service { get; set; }
}
