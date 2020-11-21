using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.Core.Dto;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystem.Web.Controllers
{ 
    public class AuthController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly JwtSettings jwtSettings;

        public AuthController(UserManager<ApplicationUser> userManager, JwtSettings jwtSettings, SignInManager<ApplicationUser> signInManager)
        {
            this.jwtSettings = jwtSettings;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationDto registrationDto)
        {
            var existingUser = await userManager.FindByEmailAsync(registrationDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new ErrorDto()
                {
                    Message = "User already exists"
                });

            }
            var newUser = new ApplicationUser()
            {

                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                UserName = registrationDto.Email
            };

            var createdUser = await userManager.CreateAsync(newUser, registrationDto.Password);

            if (!createdUser.Succeeded)
            {
                return Problem("Problem creating user account");
            }

            JwtSecurityTokenHandler jwtTokenHandler;
            SecurityToken token;
            GenerateToken(newUser, out jwtTokenHandler, out token);

            return Ok(new UserDto()
            {
                Email = newUser.Email,
                AccessToken = jwtTokenHandler.WriteToken(token)
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest(new ErrorDto()
                {
                    Message = "User does not exist"
                });
            }

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            JwtSecurityTokenHandler jwtTokenHandler;
            SecurityToken token;
            if (!result)
            {
                return BadRequest(new ErrorDto()
                {
                    Message = "Username or Password is not valid"
                });
            }
            GenerateToken(user, out jwtTokenHandler, out token);

            return Ok(new UserDto()
            {
                Email = user.Email,
                AccessToken = jwtTokenHandler.WriteToken(token)
            });
        }

        [HttpGet("authSchemes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAuthSchemes()
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Auth", new { ReturnUrl = "" });
            var result = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            return new ChallengeResult("Google", properties);
        }

        private void GenerateToken(IdentityUser newUser, out JwtSecurityTokenHandler jwtTokenHandler, out SecurityToken token)
        {
            jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim (JwtRegisteredClaimNames.Sub, newUser.Email),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
                new Claim (JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim ("id", newUser.Id)

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            token = jwtTokenHandler.CreateToken(tokenDescriptor);
        }

    }
}