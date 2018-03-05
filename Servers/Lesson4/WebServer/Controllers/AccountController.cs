using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebServer.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using WebServer.ModelViews;
using static WebServer.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {

        DbServerContext _context;
        UserManager<IdentityUser> _userManager;

        public AccountController(DbServerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUser(string userId)
        {
           return Ok(User.Identity.Name);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]IdentityUser user, string password)
        {
            var result =  await this._userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Ok(await _userManager.FindByNameAsync(user.UserName));
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody]JwtLoginModelView model)
        {
            var identity = await GetIdentity(model.Username, model.Password);

            if (identity == null)
            {
                return BadRequest(new { Error = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // create JWT-token
            var jwt = new JwtSecurityToken(
                    issuer: JWT_ISSUER,
                    audience: JWT_AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(JWT_LIFETIME)),
                    signingCredentials: new SigningCredentials(JwtSymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);

        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "SimpleUser")
                };

                var claimsIdentity = new ClaimsIdentity
                (
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                );

                return claimsIdentity;
            }

            return null; // No user
        }
    }
}