namespace Api.Models;

public class Owned_game
{
    public int User_id { get; set; }
    public int Game_id { get; set; }
    
    public User User { get; set; }
    public Game Game { get; set; }
}