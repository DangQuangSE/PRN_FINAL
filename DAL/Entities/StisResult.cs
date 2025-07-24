using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StisResult
{
    public int ResultId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Note { get; set; }

    public string? PdfResultUrl { get; set; }

    public string? ReferenceRange { get; set; }

    public DateTime? ResultDate { get; set; }

    public string? ResultText { get; set; }

    public string? ResultValue { get; set; }

    public string? TestCode { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? BookingId { get; set; }

    public virtual StisBooking? Booking { get; set; }
}
