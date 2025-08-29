using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Domain.Dtos;
using TaskManagement.Domain.UserManagement;
using TaskManagement.Persistence;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TaskManagementDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TaskManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // implement the register endpoint
        // validate then inputs
        // check if the email already exist in the db.
        // create the user object
        // hash the password - BCrypt.hashpassword()
        // save user to db
        // return success message to frontend

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // check if the db email is equal to the frontend email
            var isUserExist = await _context.Users.AnyAsync(em => em.Email == userDto.Email);

            if (isUserExist)
            {
                return BadRequest("Email is already in use.");
            }

            var user = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                AccountType = userDto.AccountType,
                UserStatus = UserStatus.Active,
                Gender = userDto.Gender,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User Registered Successfully.", UserId = user.Id });
        }



        // Implementation for login endpoint
        // check if the email and password are provided (validation)
        // find user by email in the db
        // check if the account is suspended (option)
        // generate token - function to generate the token, define the expiration time
        // rerturn a mesage(token)
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.PasswordHash))
            {
                return BadRequest("Email and password are required");
            }

            var user = await _context.Users.FirstOrDefaultAsync(em => em.Email == loginRequest.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            if (!VerifyPassword(loginRequest.PasswordHash, user.PasswordHash!))
            {
                return Unauthorized("Invalid email or password");
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Id = user.Id,
                AccountType = user.AccountType
            });
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<CurrentUserDto> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var email = identity.FindFirst(ClaimTypes.Email).Value;
            var accountType = identity.FindFirst(ClaimTypes.Role).Value;

            var currentUser = new CurrentUserDto
            {
                Id = userId,
                Email = email,
                AccountType = accountType
            };

            return Ok(currentUser);
        }

        private string GenerateJwtToken(User user)
        {
            //Claim
            // 1. attach the user id to the name identifyer in the claim
            // 2. attach the  user email to the claim email
            // 3. user firstname to the claim name
            // 4. user firstname and last name to the given name in the claim
            // 5. user account type to the role in the claim
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.AccountType.ToString())
            };

            // Keys
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["key"]));

            // Credential
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Assign the claims, the key, creds to the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddHours(1));

            // Final is the JwtSecurity token handler that creates the token of strings
            var newToken = new JwtSecurityTokenHandler().WriteToken(token);

            return newToken;
        }



        private bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, storedHash);

            return isPasswordValid;
        }








        // Go to the User controller and protect the endpoints
        // .............Project controller
    }
}
