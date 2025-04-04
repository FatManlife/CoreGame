using System.Text.RegularExpressions;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Api.Services;
using Api.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtService jwtService, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            
            if(string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password)) return NotFound() ;

            var user = await _userRepository.GetUserByEmail(loginDto.Email);
            
            if(user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Hashed_password)) return NotFound();

            var authToken = _jwtService.CreateCookie(user);
            
            return Ok(authToken);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody]RegisterDto registerDto)
        {
            
            //Validation
            if(!RegexValidator.Validate("Username",registerDto.Username)) return BadRequest( new { message = "Username is Invalid!" });
            
            if(!RegexValidator.Validate("Email",registerDto.Email)) return BadRequest( new { message = "Email is Invalid!" });
            
            if(!RegexValidator.Validate("Password",registerDto.Hashed_password)) return BadRequest( new { message = "Password is Invalid!" });
            
            registerDto.Date_created = DateTime.Now;
            
            var userEmail = await _userRepository.GetUserByRegister(registerDto.Username ,registerDto.Email);
                
            if(userEmail != null) return BadRequest(new {message = "User already exists!" });
            
            _mapper.Map<User>(registerDto);
            
            registerDto.Hashed_password = BCrypt.Net.BCrypt.HashPassword(registerDto.Hashed_password);
            
            //UserRepository;
            var newUser = _mapper.Map<User>(registerDto);
            var createdUser = await _userRepository.CreateUser(newUser);
            
            var authToken = _jwtService.CreateCookie(newUser);
            
            return Ok(authToken);
        }
        
        
    }
}
