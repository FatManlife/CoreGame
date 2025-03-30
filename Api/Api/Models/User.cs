namespace Api.Models;

public class User
{
    public int Id { get; set; }
    public int Role { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public string Hashed_password { get; set; }
    public DateTime Date_created { get; set; }
    
    public ICollection<Owned_game> Owned_games { get; set; }
    public ICollection<Game> Games { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Replie> Replies { get; set; }
    
}