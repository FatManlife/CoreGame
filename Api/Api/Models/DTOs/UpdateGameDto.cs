namespace Api.Models.DTOs;

public class UpdateGameDto
{
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? File_path { get; set; }
    public int? Status { get; set; }
    public string? CoverImage { get; set; }
    
    public ICollection<int> GenresId { get; set; }
    public ICollection<int> ModesId { get; set; }
    public ICollection<int> PlatformsId { get; set; }
    
}