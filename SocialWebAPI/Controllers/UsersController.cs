using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.DTOs;
using SocialWebAPI.Entities;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseAPIController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _userRepository.GetMembersAsync());

            //var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            //return Ok(usersToReturn);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUserName(string userName)
        {
            return await _userRepository.GetMemberByNameAsync(userName);
            //var usersToReturn = _mapper.Map<MemberDto>(user);
            //return usersToReturn;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppUser>> GetUsersById(int id)
        //{
        //    return await _userRepository.GetByIdAsync(id);
        //}

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetByUserNameAsync(username);

            if (user == null) return NotFound();

            _mapper.Map(memberUpdateDto, user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Update user failed");
        }
    }
}
