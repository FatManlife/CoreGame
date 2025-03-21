namespace Api.Models;

public class Game_ordered
{
    public int Id { get; set; }
    public int Order_id { get; set; }
    public int Game_id { get; set; }
    public decimal Price { get; set; }
    
    public Game Game { get; set; }
    public Order Order { get; set; }
}