using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ConsultantFeedback
{
    public int FeedbackId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Rating { get; set; }

    public int? ConsultantProfileId { get; set; }

    public int? BookingId { get; set; }

    public int? CustomerId { get; set; }

    public virtual ConsultationBooking? Booking { get; set; }

    public virtual ConsultantProfile? ConsultantProfile { get; set; }

    public virtual User? Customer { get; set; }
}
