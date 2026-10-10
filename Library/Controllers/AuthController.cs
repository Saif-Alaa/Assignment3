using Library.Dtos;
using Library.Models;
using Library.Work;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration config;
        public AuthController(
            IUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher,
            IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.passwordHasher = passwordHasher;
            this.config = config;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto dto) 
        {
       

            var exists = await unitOfWork.Users.UserNameExists(dto.UserName);

            if (exists)
            {
                return Conflict(new
                {
                    message = "UserName already exists."
                });
            }

        

            var user = new User
            {
                Username = dto.UserName,
                Role = "Member",
            };

         
            user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);


            unitOfWork.Users.Create(user);
            unitOfWork.Save();

            return Ok(new
            {
                message = "User registered successfully."
            });

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            //var user = await unitOfWork.Users
            //    .GetByUserName(dto.UserName);

            var user = await unitOfWork.Users.GetByUserName(dto.UserName);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

        

            var result = passwordHasher.VerifyHashedPassword(user , user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password."
                });
            }

            var expiration = DateTime.UtcNow.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Role , user.Role)
            };


            var Key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"])
                );


            var credentials = new SigningCredentials(Key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new LoginResponseDto
            {
                Token = tokenString,
                Expiration = expiration
            });
        }
    }
}
