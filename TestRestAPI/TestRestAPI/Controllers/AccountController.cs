using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestRestAPI.Model;
using TestRestAPI.Models;

namespace TestRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase

    {
        public AccountController(UserManager<User> userManager ,IConfiguration configuration)
        {
            _userManager = userManager;
           this.configuration = configuration;
        }

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;


        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUser(dtoNewUser NewUser)
        {
            if (ModelState.IsValid) {

                User user = new();
                {
                    user.Email = NewUser.Email;
                    user.UserName = NewUser.userName;

                };
                IdentityResult result = await _userManager.CreateAsync(user, NewUser.Password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                    foreach (var item in result.Errors) {
                        ModelState.AddModelError("", item.Description);
                    }
            } 

            return BadRequest(ModelState);  
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] dtoLoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginUser.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                        claims.Add(new Claim(ClaimTypes.Role, role));

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: configuration["JWT:Issuer"],
                        audience: configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: creds
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }

                return Unauthorized("Invalid username or password");
            }

            return BadRequest(ModelState);
        }


    }
}
