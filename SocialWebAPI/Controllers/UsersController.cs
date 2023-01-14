using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialWebAPI.Db;
using SocialWebAPI.Entities;
using SocialWebAPI.Interfaces;

namespace SocialWebAPI.Controllers
{
    [Authorize]
    public class UsersController: BaseAPIController
    {
        private readonly IUserRepository _userRepository;


        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsersAsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AppUser>> GetUsersByUserName(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<AppUser>> GetUsersById(int id)
        //{
        //    return await _userRepository.GetByIdAsync(id);
        //}
    }
}
