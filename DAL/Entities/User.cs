using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Address { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Provider { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UserImageUrl { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<ConsultantFeedback> ConsultantFeedbacks { get; set; } = new List<ConsultantFeedback>();

    public virtual ICollection<ConsultantProfile> ConsultantProfiles { get; set; } = new List<ConsultantProfile>();

    public virtual ICollection<ConsultationBooking> ConsultationBookingConsultants { get; set; } = new List<ConsultationBooking>();

    public virtual ICollection<ConsultationBooking> ConsultationBookingCustomers { get; set; } = new List<ConsultationBooking>();

    public virtual ICollection<MenstrualCycle> MenstrualCycles { get; set; } = new List<MenstrualCycle>();

    public virtual ICollection<Pill> Pills { get; set; } = new List<Pill>();

    public virtual ICollection<Question> QuestionAnswerByNavigations { get; set; } = new List<Question>();

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();

    public virtual ICollection<Question> QuestionConsultants { get; set; } = new List<Question>();

    public virtual ICollection<Question> QuestionCustomers { get; set; } = new List<Question>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<StisBooking> StisBookings { get; set; } = new List<StisBooking>();
}
