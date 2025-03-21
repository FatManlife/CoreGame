namespace Api.Models;

public class Spec
{
    public int Id { get; set;}
    public int Game_id { get; set; }
    public string Os { get; set;}
    public string Proccessor { get; set;}
    public string Memory {get;set;}
    public string Graphics {get;set;}
    public string Storage { get; set;}
    public string Additional_notes{get;set;}
    public bool Is_Minimum { get; set;}
    
    public Game Game { get; set; }
}