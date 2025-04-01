using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class GameRepository: IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Game>> GetAllGames()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<Game> GetGameById(int id)
    {
        return await _context.Games
            .Include(u => u.Developer)
            .Include(u => u.Genres)
            .Include(g => g.Platforms)
            .Include(g => g.Publisher)
            .Include(g => g.Modes)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<Game> GetGameByTitle(string title)
    {
        return await _context.Games.FirstOrDefaultAsync(g => g.Title == title);
    }

    public async Task CreateGame(Game game)
    {
        await _context.Images.AddRangeAsync(game.Images);
        await _context.Specs.AddRangeAsync(game.Specs); 
        await _context.Games.AddAsync(game); 
        await _context.SaveChangesAsync(); 
    }
    public async Task UpdateGame(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
    }

    public Task DeleteGame(Game game)
    {
        _context.Games.Remove(game);
        return _context.SaveChangesAsync();
    }
}