namespace Api.Models;

public class Mode
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Game> Games { get; set; }
}