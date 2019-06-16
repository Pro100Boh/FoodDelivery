using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FoodDeliveryServer.Entities;
using FoodDeliveryServer.Infrastructure;
using FoodDeliveryServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodDeliveryServer.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly FoodDeliveryContext _dbContext;

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager,
            FoodDeliveryContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel registrationModel)
        {
            var user = new User
            {
                UserName = registrationModel.Email,
                Email = registrationModel.Email
            };

            var identityResult = await _userManager.CreateAsync(user, registrationModel.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var e in identityResult.Errors)
                    ModelState.TryAddModelError(e.Code, e.Description);

                return BadRequest(ModelState);
            }

            //await db.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            await _dbContext.SaveChangesAsync();

            return Ok("Account created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

            if (!signInResult.Succeeded)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            return GenerateJwtToken(user.Email, user.Id);
        }

        private IActionResult GenerateJwtToken(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
