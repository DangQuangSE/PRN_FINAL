using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StisBooking
{
    public int BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Note { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? ResultedAt { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CustomerId { get; set; }

    public int? ServiceId { get; set; }

    public virtual User? Customer { get; set; }

    public virtual StisService? Service { get; set; }

    public virtual ICollection<StisFeedback> StisFeedbacks { get; set; } = new List<StisFeedback>();

    public virtual ICollection<StisInvoice> StisInvoices { get; set; } = new List<StisInvoice>();

    public virtual ICollection<StisResult> StisResults { get; set; } = new List<StisResult>();
}
