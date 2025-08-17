using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.UserManagement;
using TaskManagement.Persistence;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskManagementDbContext _context;

        public UserController(TaskManagementDbContext context)
        {
            _context = context;
        }
        // Create Operation
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            // Todo: Make email unique
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            await _context.Users.AddAsync(user); // Users is the table name in the dbset
            await _context.SaveChangesAsync();
            return Ok("User Created Successfully");
        }

        // Fetch by Id Operation
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User is not Found");
            }
            return user;
        }


        // Fech All Operation
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers() 
        { 
            return await _context.Users.ToListAsync();
        }


    }
}
