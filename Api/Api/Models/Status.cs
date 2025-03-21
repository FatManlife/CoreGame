namespace Api.Models;

public enum StatusName
{
    Early_access,
    Alpha,
    Beta,
    Full_release
    
}

public class Status
{
    public int Id { get; set; }
    public int Game_id { get; set; }
    public StatusName Name { get; set; }
    public DateTime Release_date { get; set; }
 
    public Game Game { get; set; }
}