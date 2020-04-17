using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using app.Data;
using app.Models;
using app.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace app.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(
            AppDbContext db, 
            IConfiguration configuration, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager
        )
        {
            this.db = db;
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        
        /// <summary>
        /// Gets access token.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> GetTokenAsync([FromBody]LoginViewModel model, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var isValidPassword = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!isValidPassword)
            {
                return BadRequest("Invalid username or password.");
            }

            var utcNow = DateTime.UtcNow;
            var claims = new Claim[] 
            { 
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"), 
                new Claim(ClaimTypes.Name, $"{user.UserName}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetSection("JwtOptions:SigningKey").Get<string>()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = utcNow.AddDays(this.configuration.GetSection("JwtOptions:Duration").Get<double>());
            var issuer = this.configuration.GetSection("JwtOptions:Issuer").Get<string>();
            var audience = this.configuration.GetSection("JwtOptions:Audience").Get<string>();
            var token = new JwtSecurityToken(issuer, audience, claims, expires: expires, signingCredentials: creds);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(accessToken);
        }
    }
}