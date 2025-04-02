using Api.Interfaces;
using Api.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowGameDto>> GetGame(int id)
        {
            var game = await _gameRepository.GetGameById(id);
            
            if (game == null) return NotFound();
            
            return Ok(_mapper.Map<ShowGameDto>(game));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowGameDto>>> GetGames()
        {
            return Ok(_mapper.Map<IEnumerable<ShowGameDto>>(await _gameRepository.GetGames()));
        } 
        
    }
}
