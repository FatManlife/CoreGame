using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models;

public class Game
{
    public int Id { get; set; }
    public int Publisher_id { get; set; }
    public int Developer_id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Status { get; set; }
    public string CoverImage { get; set; }
    public string File_path { get; set; }
    
    public ICollection<Owned_game> Owned_games { get; set; }
    public User Developer { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<Mode> Modes { get; set; }
    public ICollection<Game_ordered> Games_ordered { get; set; }
    public ICollection<Platform> Platforms { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<Spec> Specs { get; set; }
    public ICollection<Image> Images { get; set; }
}