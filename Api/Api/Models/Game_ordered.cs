using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Game_ordered
{
    public int Id { get; set; }
    public int Order_id { get; set; }
    public int Game_id { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    
    public Game Game { get; set; }
    public Order Order { get; set; }
}