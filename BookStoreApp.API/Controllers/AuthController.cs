using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager) : ControllerBase
    {

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            logger.LogInformation($"Registration attempt for {userDto.Email}");
            try
            {
                if (!IsRoleValid(userDto.Role))
                {
                    return BadRequest("Invalid Role");
                }

                var user = mapper.Map<ApiUser>(userDto);
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, GetStandardRole(userDto.Role));
                return Accepted();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            logger.LogInformation($"Login attempt for {loginUser.Email}");
            try
            {
                var user = await userManager.FindByEmailAsync(loginUser.Email);
                var passwordValid = await userManager.CheckPasswordAsync(user, loginUser.Password);

                if (user==null || passwordValid == false)
                {
                    return NotFound();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
            return Ok();
        }

        private bool IsRoleValid(string role)
        {
            if (role.ToLower() == "user" || role.ToLower() == "administrator")
                return true;
            return false;
        }

        private string GetStandardRole(string role)
        {
            if (role.ToLower() == "administrator")
                return "Administrator";

            return "User";
        }
    }
}
