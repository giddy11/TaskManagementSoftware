using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using Microsoft.EntityFrameworkCore;
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
        // TODO: prevent duplicate emails
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // Fetch By Id Operation
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            // check if the user id exist in the db
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // Fetch All Operation
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // Update Operation


        // Delete Operation

    }
}
