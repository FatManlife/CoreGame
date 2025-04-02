using Api.Interfaces;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository;

public class OrderRepository: IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task CreateGameOrders(List<Game_ordered> gameOrderes)
    {
        await _context.Games_ordered.AddRangeAsync(gameOrderes);
        await _context.SaveChangesAsync();
    }

    public async Task CreateOwnedGame(List<Owned_game> ownedGames)
    {
        await _context.Owned_games.AddRangeAsync(ownedGames);
        await _context.SaveChangesAsync();
    }
}