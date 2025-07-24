using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public virtual BlogPost? Post { get; set; }

    public virtual User? User { get; set; }
}
