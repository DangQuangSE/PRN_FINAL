using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ProfileDetail
{
    public int DetailId { get; set; }

    public string? Description { get; set; }

    public string DetailType { get; set; } = null!;

    public DateOnly? FromDate { get; set; }

    public DateOnly? IssuedDate { get; set; }

    public string? Organization { get; set; }

    public string? Title { get; set; }

    public DateOnly? ToDate { get; set; }

    public int ProfileId { get; set; }

    public virtual ConsultantProfile Profile { get; set; } = null!;
}
