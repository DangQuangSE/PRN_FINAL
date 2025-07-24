using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class QuestionComment
{
    public int CommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public virtual Question? Question { get; set; }

    public virtual User? User { get; set; }
}
