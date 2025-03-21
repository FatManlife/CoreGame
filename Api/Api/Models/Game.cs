namespace Api.Models;

public class Game
{
    public int Id { get; set; }
    public int Publisher_id { get; set; }
    public int Developer_id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string File_path { get; set; }
    
    public ICollection<Owned_game> Owned_games { get; set; }
    public User Developer { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public ICollection<Mode> Modes { get; set; }
    public ICollection<Game_ordered> Games_ordered { get; set; }
    public ICollection<Platform> Platforms { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<Spec> Specs { get; set; }
    public ICollection<Dlc> Dlcs { get; set; }
    public ICollection<Status> Statuses { get; set; }
}