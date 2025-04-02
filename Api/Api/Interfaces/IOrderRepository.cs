using Api.Models;

namespace Api.Interfaces;

public interface IOrderRepository
{
    public Task CreateOrder(Order order);
    public Task CreateGameOrders(List<Game_ordered> gameOrderes);
    public Task CreateOwnedGame(List<Owned_game> ownedGames);
}