namespace Api.Models;

public class Dlc
{
    public int Id { get; set; }
    public int Game_id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string File_path { get; set; }
    public DateTime Publish_date { get; set; }
    
    public Game Game { get; set; }
}