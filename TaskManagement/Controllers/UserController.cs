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
        // maryann/todo/user
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
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                // check if the user id exist
                var userExist = await _context.Users.FindAsync(id);

                if (userExist == null)
                {
                    return NotFound();
                }

                //_context.Entry(user).State = EntityState.Modified;



                userExist.Email = user.Email;
                userExist.FirstName= user.FirstName;
                userExist.LastName = user.LastName;
                userExist.Gender = user.Gender;
                userExist.AccountType = user.AccountType;


                // step one: locate the line that will give a potential exception
                // step 2: wrap that line in  a try
                // step 3: catch that exception and return a meaninful response message

                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            } catch (InvalidOperationException)
            {
                return BadRequest();
            }

            

            return NoContent();

        }


        // Delete Operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new {message = $"User with id {id} doesn't exist"});
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
