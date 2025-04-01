namespace Api.Models.DTOs;

public class ShowGameDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string File_path { get; set; }
    
    public string Developer { get; set; }
    public string PublisherName { get; set; }
    public ICollection<string> GenreNames { get; set; } 
    public ICollection<string> ModeNames { get; set; }   
    public ICollection<string> PlatformNames { get; set; }
}