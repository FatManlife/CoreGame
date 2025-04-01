using Api.Models;

namespace Api.Interfaces;

public interface IGameRepository
{
    public Task<ICollection<Game>> GetAllGames();
    public Task<Game> GetGameById(int id);
    
    public Task<Game> GetGameByTitle(string title);
    public Task CreateGame(Game game);
    public Task UpdateGame(Game game);
    public Task DeleteGame(Game game);
}