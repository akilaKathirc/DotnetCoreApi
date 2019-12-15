using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Dtos;
using WebApplication2.Helpers;

namespace WebApplication2.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}",Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user =await _repo.GetUser(id);
            var UserDetail = _mapper.Map<UserForDetailedDTO>(user);
            return Ok(UserDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();
            var UsersToReturn = _mapper.Map < IEnumerable<UserForListDto>>(users);
            return Ok(UsersToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UdateUser(int id,UserForUpdateDto userForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = _repo.GetUser(id);
             _mapper.Map(userForUpdateDto, userFromRepo.Result);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating userwith {id} is not successful !!!!");

        }

    }
}