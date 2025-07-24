using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Status { get; set; }

    public string? Email { get; set; }

    public bool? OtpVerified { get; set; }

    public string? Password { get; set; }

    public string? Otp { get; set; }

    public DateTime? ResetOtpExpiry { get; set; }

    public string? UserName { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
