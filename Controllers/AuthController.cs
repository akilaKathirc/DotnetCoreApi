using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo,IConfiguration config, IMapper mapper)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            var username = userToRegisterDto.userName.ToLower();
            if (await _repo.UserExists(userToRegisterDto.userName))
                return BadRequest("User already exists");

            var userToCreate = _mapper.Map<Users>(userToRegisterDto);

            var createdUser = await _repo.Register(userToCreate, userToRegisterDto.passWord);

            var userToReturn = _mapper.Map<UserForDetailedDTO>(createdUser);
            return CreatedAtRoute("GetUser",new { controller = "Users", id= createdUser.Id }, userToReturn);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDto userToLoginDto)
        {
            var userFromRepo =await  _repo.Login(userToLoginDto.UserName.ToLower(), userToLoginDto.password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescriptor);
            var loggedInUuser = _mapper.Map<UserForListDto>(userFromRepo);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user= loggedInUuser
            });

        }
    }
}