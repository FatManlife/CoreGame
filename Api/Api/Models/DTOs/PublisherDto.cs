namespace Api.Models.DTOs;

public class PublisherDto
{
    public decimal Royalty { get; set; }
    public string Name { get; set; }
    public DateTime? Created_on { get; set; }
    public string Description { get; set; }
}