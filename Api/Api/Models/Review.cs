namespace Api.Models;

public class Review
{
    public int Id { get; set; }
    public int User_id { get; set; }
    public int Game_id { get; set; }
    public string Content { get; set; }
    public DateTime Created_on { get; set; }
    
    public User User { get; set; }
    public Game Game { get; set; }
    public ICollection<Replie> Replies { get; set; }
    
}