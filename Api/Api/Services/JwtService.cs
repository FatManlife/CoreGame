using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Identity;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Rules;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Api.Services;

public class JwtService
{
     private readonly IConfiguration _configuration;
     private readonly IUserRepository _userRepository;

     public JwtService(IConfiguration config, IUserRepository userRepository)
     {
          _configuration = config;
          _userRepository = userRepository;
     }

     public async Task<string> CreateCookie(User user)
     {
          
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
          
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

          string role;
          
          switch (user.Role)
          {
               case 1:
                    role = "customer"; break;
               case 2:
                    role = "developer"; break;
               case 3:
                    role = "admin"; break;
               default:
                    role = "customer"; break;
          }
          
          var token = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: new List<Claim>
               {
                    new Claim("userId",user.Id.ToString()),
                    new Claim("name", user.Username),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(IdentityData.RoleUserClaimName, role)
               },
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds);

          return new JwtSecurityTokenHandler().WriteToken(token);
     }
}