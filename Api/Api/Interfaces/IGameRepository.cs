using Api.Models;

namespace Api.Interfaces;

public interface IGameRepository
{
    public Task<Game> GetGameById(int id);
    public Task<ICollection<Game>> GetGames();
    public Task<Game> GetGameByTitle(string title);
    public Task CreateGame(Game game);
    public Task UpdateGame(Game game);
    public Task DeleteGame(Game game);
    public Task<ICollection<Game>> GetGamesById(List<int> gameIds);
    
}