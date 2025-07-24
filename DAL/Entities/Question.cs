using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public string? Answer { get; set; }

    public DateTime? AnsweredAt { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? Title { get; set; }

    public int? AnswerBy { get; set; }

    public int? ConsultantId { get; set; }

    public int? CustomerId { get; set; }

    public virtual User? AnswerByNavigation { get; set; }

    public virtual User? Consultant { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<QuestionComment> QuestionComments { get; set; } = new List<QuestionComment>();
}
