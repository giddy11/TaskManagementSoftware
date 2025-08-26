using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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



        // Implementation for login endpoint
        // check if the email and password are provided (validation)
        // find user by email in the db
        // check if the account is suspended (option)
        // generate token - function to generate the token, define the expiration time
        // rerturn a mesage(token)

        // Go to the User controller and protect the endpoints
        // .............Project controller
    }
}
