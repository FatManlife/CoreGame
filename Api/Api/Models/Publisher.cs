using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Publisher
{
    public int Id { get; set; }
    [Column(TypeName = "decimal(5,2)")]
    public decimal Royalty { get; set; }
    public string Name { get; set; }
    public DateTime Created_on { get; set; }
    public string Description { get; set; }
    public string PublisherImage { get; set; }
    
    public ICollection<Game> Games { get; set; }
}