namespace Api.Models;

public class Publisher
{
    public int Id { get; set; }
    public decimal Royalty { get; set; }
    public string Name { get; set; }
    public DateTime Created_on { get; set; }
    public string Description { get; set; }
    
    public ICollection<Game> Games { get; set; }
}