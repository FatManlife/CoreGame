namespace Api.Models.DTOs;

public class ShowGameDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Status { get; set; }
    public string CoverImage { get; set; }
    public string PublisherName { get; set; }
    public string DeveloperName { get; set; }
    
    public ICollection<SpecDto> Specs { get; set; }
    public ICollection<ImageGameDto> Images { get; set; }
    public ICollection<string> GenreNames { get; set;} 
    public ICollection<string> ModeNames { get; set; }   
    public ICollection<string> PlatformNames { get; set;}
}