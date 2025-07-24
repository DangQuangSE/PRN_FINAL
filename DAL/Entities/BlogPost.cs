using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BlogPost
{
    public int PostId { get; set; }

    public string? Content { get; set; }

    public long? LikeCount { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? Status { get; set; }

    public string? Tags { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? Title { get; set; }

    public long? ViewCount { get; set; }

    public int? ConsultantId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? Consultant { get; set; }
}
