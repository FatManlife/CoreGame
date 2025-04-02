using Api.Identity;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Api.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;
        private readonly IDetailsGameRepository _detailsGameRepository;

        public DeveloperController(IGameRepository gameRepository, IMapper mapper, IDetailsGameRepository detailsGameRepository, IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _detailsGameRepository = detailsGameRepository;
        }

        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpPost("game")]
        public async  Task<IActionResult> CreateGame([FromBody]AddGameDto addGameDto)
        {
            //Validation
            if(!RegexValidator.Validate("Number",addGameDto.Publisher_id.ToString())) return BadRequest(new {message = "Publisher id must contain numbers"});
            var PublisherId = await _publisherRepository.GetPublisherById(addGameDto.Publisher_id);
            if(PublisherId == null) return BadRequest(new {message = "Publisher not found"});
            
            if(String.IsNullOrWhiteSpace(addGameDto.Title)) return BadRequest(new {message = "Title must be at least 2 characters long"}); 
            
            var validGame = await _gameRepository.GetGameByTitle(addGameDto.Title);
            
            if(validGame != null) return BadRequest(new {message = "Game already exists"});
            
            if(String.IsNullOrWhiteSpace(addGameDto.CoverImage)) return BadRequest(new {message = "Must provide a cover image"});
            
            if(addGameDto.Description.Length < 3) return BadRequest(new {message = "Description must be at least 3 characters long"});
            
            if(addGameDto.Price < 0) return BadRequest(new {message = "Price can't be negative"});
            
            if(String.IsNullOrWhiteSpace(addGameDto.File_path)) return BadRequest(new {message = "Description is required"});
            
            addGameDto.MaxSpecs.Is_Minimum = false;
            addGameDto.MinSpecs.Is_Minimum = true;

            List<Spec> currentSpecs = new List<Spec>();
            currentSpecs.Add(_mapper.Map<Spec>(addGameDto.MaxSpecs));
            currentSpecs.Add(_mapper.Map<Spec>(addGameDto.MinSpecs));

            string status;
            switch (addGameDto.Status)
            {
                case 1: status = "Early Access"; break;
                case 2: status = "Beta"; break;
                case 3: status = "FullRelease"; break;
                default:
                    return BadRequest(new {message = "Invalid status"});
            }
            
            var game = _mapper.Map<AddGameDto, Game>(addGameDto);
            
            var userIdFromCookie = User.FindFirst("userId")?.Value;

            game.Developer_id = Convert.ToInt32(userIdFromCookie);
            game.ReleaseDate = DateTime.Now;
            game.Status = status;
            game.Genres = await _detailsGameRepository.getGeneres(addGameDto.GenresId);
            game.Modes = await _detailsGameRepository.getModes(addGameDto.ModesId);
            game.Platforms = await _detailsGameRepository.getPlatforms(addGameDto.PlatformsId);
            game.Images = _mapper.Map<List<Image>>(addGameDto.Images);
            game.Specs = currentSpecs;
            game.Price = addGameDto.Price + PublisherId.Royalty;
            
            foreach (var spec in game.Specs)
            {
                spec.Game_id = game.Id;
            }
            foreach(var image in game.Images)
            {
                image.Game_id = game.Id;   
                image.IsGame = true;
            }
            
            await _gameRepository.CreateGame(game);
            
            return NoContent();
        }

        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpPut("game/{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameDto updateGameDto)
        {
            //Default Validation
            var userIdFromCookie = User.FindFirst("userId")?.Value;
            
            var game = await _gameRepository.GetGameById(id);
            
            if (game == null || game.Developer_id != Convert.ToInt32(userIdFromCookie)) return Unauthorized();
            
            //Validation
            
            if (!String.IsNullOrWhiteSpace(updateGameDto.CoverImage)) game.CoverImage = updateGameDto.CoverImage;
            
            if(updateGameDto.Description.Length > 3) game.Description = updateGameDto.Description;
            
            if(updateGameDto.Price > 0) game.Price = (decimal)updateGameDto.Price;
            
            string status;
            switch (updateGameDto.Status)
            {
                case 1:  game.Status  = "Early Access"; break;
                case 2:  game.Status  = "Beta"; break;
                case 3: game.Status = "FullRelease"; break;
                default:
                    game.Status = game.Status; break;
            }
            
            game.Genres = await _detailsGameRepository.getGeneres(updateGameDto.GenresId);
            game.Modes = await _detailsGameRepository.getModes(updateGameDto.ModesId);
            game.Platforms = await _detailsGameRepository.getPlatforms(updateGameDto.PlatformsId);
            
            
            await _gameRepository.UpdateGame(game);
            
            return NoContent();
        }

        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpDelete("game/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            //Standard Validation
            var userIdFromCookie = User.FindFirst("userId")?.Value;
            var game = await _gameRepository.GetGameById(id);
            if (game == null || game.Developer_id != Convert.ToInt32(userIdFromCookie)) return Unauthorized();
            
            //Delteion
            await _gameRepository.DeleteGame(game);
            return NoContent();
        }
        
        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpPost("game/{gameId}/image")]
        public async Task<IActionResult> AddImage(int gameId, [FromBody]ImageGameDto imageGameDto)
        {
            //Basic Validation
            var userIdFromCookie = User.FindFirst("userId")?.Value;
            var game = await _gameRepository.GetGameById(gameId);
            
            if (game == null||game.Developer_id!= Convert.ToInt32(userIdFromCookie)) 
                return Unauthorized(new { message = "You don't own this game" });

            // Add image
            if (String.IsNullOrWhiteSpace(imageGameDto.Image_url)) return BadRequest(new { message = "Invalid Image" });
            
            var newImage = _mapper.Map<Image>(imageGameDto);
            newImage.Game_id = game.Id;
            newImage.Created_on = DateTime.Now;
            newImage.IsGame = true;
            
            await _detailsGameRepository.AddImage(newImage);

            return NoContent();
        }
        
        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpDelete("game/{gameId}/image/{id}")]
        public async Task<IActionResult> DeleteImage(int gameId, int id)
        {
            //Basic Validation
            var userIdFromCookie = User.FindFirst("userId")?.Value;
            
            var game = await _gameRepository.GetGameById(gameId);
            if (game == null||game.Developer_id!= Convert.ToInt32(userIdFromCookie)) 
                return Unauthorized(new { message = "You don't own this game" });
            
            var image = await _detailsGameRepository.getImageById(id);
            if (image == null || image.Game_id != gameId) 
                return NotFound(new { message = "Image not found for this game" });
            // Delete image
            
            await _detailsGameRepository.DeleteImage(image);

            return NoContent();
        }
        
        [Authorize(Policy = IdentityData.DeveloperPolicyName)]
        [HttpPut("game/{gameId}/spec/{id}")]
        public async Task<IActionResult> UpdateSpec(int gameId, int id, [FromBody]SpecDto specDto)
        {
            //Basic Validation
            var userIdFromCookie = User.FindFirst("userId")?.Value;
            
            var game = await _gameRepository.GetGameById(gameId);
            if (game == null||game.Developer_id!= Convert.ToInt32(userIdFromCookie)) 
                return Unauthorized(new { message = "You don't own this game" });
            
            var spec = await _detailsGameRepository.getSpecById(id);
            if (spec == null || spec.Game_id != gameId) 
                return NotFound(new { message = "Spec not found for this game" });
            // Update spec
            
            _mapper.Map(specDto, spec);
            await _detailsGameRepository.updateSpec(spec);

            return NoContent();
        }
    }
}
