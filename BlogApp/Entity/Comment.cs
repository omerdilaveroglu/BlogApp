using System;

namespace BlogApp.Entity;

public class Comment
{
    public int CommentId { get; set; } 
    public string? CommentText { get; set; }
    public DateTime PublishedOn { get; set; }
    public int PostId { get; set; }
    public Post Posts { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
