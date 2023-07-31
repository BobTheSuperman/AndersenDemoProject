using Domain.Core.Entities;
using Domain.Core.Models.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Users;

namespace AndersenDemoProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAllOrigin")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            var response = await _userService.GetUser(id);

            if (response.Result.Succeeded)
            {
                return Ok(response.User);
            }

            return StatusCode(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetUsers();

            if (response.Result.Succeeded)
            {
                return Ok(response);
            }

            return StatusCode(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserModel userModel)
        {
            var response = await _userService.CreateUserAsync(userModel);

            if (response.Result.Succeeded)
            {
                return Ok(response.Id);
            }

            return StatusCode(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserModel userModel)
        {
            var response = await _userService.UpdateUser(id, userModel);

            if(response.Result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);

            if (response.Result.Succeeded)
            {
                return Ok();
            }

            return StatusCode(response);
        }
    }
}
