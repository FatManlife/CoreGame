namespace Api.Models.DTOs;

public class AddGameDto
{
    public int Publisher_id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string File_path { get; set; }
    public int Status { get; set; }
    public string CoverImage { get; set; }
    
    public SpecDto? MaxSpecs { get; set; }
    public SpecDto? MinSpecs { get; set; }
    public ICollection<ImageGameDto>? Images { get; set; }
    public ICollection<int> GenresId { get; set; }
    public ICollection<int> ModesId { get; set; }
    public ICollection<int> PlatformsId { get; set; }
}