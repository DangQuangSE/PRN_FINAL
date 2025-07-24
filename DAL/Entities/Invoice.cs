using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public string? Currency { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? PaymentMethod { get; set; }

    public double? RefundAmount { get; set; }

    public string? RefundStatus { get; set; }

    public double? TotalAmount { get; set; }

    public string? TransactionId { get; set; }

    public int? BookingId { get; set; }

    public virtual ConsultationBooking? Booking { get; set; }
}
