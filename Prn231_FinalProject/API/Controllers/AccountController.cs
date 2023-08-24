using API.Extensions;
using API.Models;
using API.Models.Request;
using API.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly QuanLyKhoContext _dbcontext;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            QuanLyKhoContext dbcontext,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        [HttpPost("GetToken")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                {
                    return NotFound();
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Login failed");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login.Email)
                    };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["Jwt:ExpiryInDays"]));

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(token);

                return new LoginResponse
                {
                    Token = tokenString
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return Conflict(ModelState);
                }

                var newUser = new User
                {
                    UserName = request.Email,
                    Password = request.Password,
                };

                var createResult = await _userManager.CreateAsync(newUser, request.Password);
                if (!createResult.Succeeded)
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, "user");
                if (!addToRoleResult.Succeeded)
                {
                    foreach (var error in addToRoleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Created(string.Empty, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                // Remove the token cookie from the client
                Response.Cookies.Delete("AccessToken");
                // Invalidate the user's token by adding it to a blacklist
                CustomMiddleware.BlackListTokens.Add(ApiContext.Current.Token, DateTime.Now);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully and return an appropriate response
                return StatusCode(500, $"Logout failed: {ex.Message}");
            }
        }

    }
}