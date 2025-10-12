using System;

namespace BlogApp.Entity;

public class Comment
{

    public int CommentId { get; set; } 
    
    public string? CommentText { get; set; }
    public DateTime PublishedOn { get; set; }
    public Post Posts { get; set; } = null!;
    public User User { get; set; } = null!;
}
