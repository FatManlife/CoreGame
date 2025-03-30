using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Api.Identity;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Api.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(id));

            if (user == null) return NotFound();
            
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto UserDto)
        {
            var userIdFromCookie = User.FindFirst("userId")?.Value; 
            
            if (string.IsNullOrEmpty(userIdFromCookie) || userIdFromCookie != id.ToString())
                return Forbid();
                    
            var user = await _userRepository.GetUserById(id);   
            
            if(user == null) return NotFound();

            if (UserDto.Username == "") UserDto.Username = user.Username;
            else if(!RegexValidator.Validate("Username",UserDto.Username)) return BadRequest( new { message = "Username is Invalid!" });
            
            if (UserDto.Email == "") UserDto.Email = user.Email;
            else if(!RegexValidator.Validate("Email",UserDto.Email)) return BadRequest( new { message = "Email is Invalid!" });
            
            var validUser = await _userRepository.GetUserByRegister(UserDto.Username ,UserDto.Email);
                
            if(validUser != null) return BadRequest(new {message = "User already exists!" });
            
            if (UserDto.Phone == "") UserDto.Phone = user.Phone;
            else if(!RegexValidator.Validate("Phone",UserDto.Phone)) return BadRequest( new { message = "Phone is Invalid!" });
            
            await _userRepository.UpdateUser(_mapper.Map(UserDto, user));
            
            return Ok(new {message = "User updated successfully!"});
        }
        
        [Authorize(Policy = IdentityData.RoleUserPoliciyName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            
            if(user == null) return NotFound();
            
            await _userRepository.DeleteUser(user);

            return Ok(new {message = "User deleted successfully!"});
        }
        
    }
}
