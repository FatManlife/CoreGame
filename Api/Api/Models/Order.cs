using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Order
{
    public int Id { get; set; }
    public int User_id { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total_price { get; set; }
    public DateTime Created_on { get; set; }
    
    public User User { get; set; }
    public ICollection<Game_ordered> Games_ordered { get; set; }
}