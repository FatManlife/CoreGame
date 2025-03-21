namespace Api.Models;

public enum EntityType
{
    publisher,
    game
}


public class Image
{
    public int Id { get; set; }
    public int Entity_id { get; set; }
    public EntityType Entity_type { get; set; }
    public string Image_url { get; set; }
    public string Alt_text { get; set; }
    public DateTime Created_on { get; set; }
    
}
