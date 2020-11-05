using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using GamesAndFriends.Api.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Application.Dtos.User;

namespace GamesAndFriends.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IUserApplication _application;
        private readonly AppSettings _appSettings;

        public AuthorizeController(IUserApplication application, IOptions<AppSettings> appSettings)
        {
            this._application = application;
            this._appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(await this._application.GetAllAsync());
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateDto model)
        {
            if (!(await _application.AuthenticateAsync(model))) 
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var user = await _application.GetAsync(model.Username);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {
                username = model.Username,
                token = tokenString
            });
        }
    }
}