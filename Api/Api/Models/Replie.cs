namespace Api.Models;

public class Replie
{
    public int Id { get; set; }
    public int Review_id { get; set; }
    public int User_id { get; set; }
    public string Content { get; set; }
    public DateTime Created_on { get; set; }
    
    public User User { get; set; }
    public Review Review { get; set; }
}