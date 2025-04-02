using Api.Identity;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, IGameRepository gameRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        [Authorize( Policy = IdentityData.CustomerPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto createOrderDto)
        {
            var userId = User.FindFirst("userId")?.Value;
            
            var games = await _gameRepository.GetGamesById(createOrderDto.GameIds);

            if (games == null)
            {
                return NotFound();
            }
            
            decimal newTotalPrice = games.Sum(game => game.Price);
            
            Order newOrder = new Order()
            {
                User_id = Convert.ToInt32(userId),
                Created_on = DateTime.Now,
                Total_price = newTotalPrice,
            };
            
            await _orderRepository.CreateOrder(newOrder);

            var gameOrders = games.Select(game => new Game_ordered
            {
                Order_id = newOrder.Id,
                Game_id = game.Id,
                Price = game.Price
            }).ToList();
            
            await _orderRepository.CreateGameOrders(gameOrders);

            var owned_games = games.Select(game => new Owned_game
            {
                Game_id = game.Id,
                User_id = newOrder.User_id
            }).ToList();
            
            await _orderRepository.CreateOwnedGame(owned_games);
            
            return NoContent();
        }
    }
}
