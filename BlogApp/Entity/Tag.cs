using System;
namespace BlogApp.Entity;


public enum TagColors
{
    primary,
    secondary,
    success,
    danger,
    warning
}


public class Tag
{
    public int TagId { get; set; }
    public string? Text { get; set; }
    public string Url { get; set; } = string.Empty;
    
    public TagColors? Color { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();
}
