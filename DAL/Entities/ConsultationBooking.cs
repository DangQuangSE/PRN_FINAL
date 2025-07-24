using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ConsultationBooking
{
    public int BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? MeetLink { get; set; }

    public string? Note { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Status { get; set; }

    public int? ConsultantId { get; set; }

    public int? CustomerId { get; set; }

    public virtual User? Consultant { get; set; }

    public virtual ICollection<ConsultantFeedback> ConsultantFeedbacks { get; set; } = new List<ConsultantFeedback>();

    public virtual User? Customer { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
