namespace Api.Models;

public class Image
{
    public int Id { get; set; }
    public int Game_id { get; set; }
    public string Image_url { get; set; }
    public string Alt_text { get; set; }
    public DateTime Created_on { get; set; }
    public bool IsGame { get; set; }
    public Game Game { get; set; }
}
