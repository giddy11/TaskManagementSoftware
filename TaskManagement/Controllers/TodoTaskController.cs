using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.TodoTasks;
using TaskManagement.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly TaskManagementDbContext _context;

        public TodoTaskController(TaskManagementDbContext context)
        {
            _context = context;
        }

        // Create Operation
        [HttpPost]
        public async Task<ActionResult<TodoTask>> CreateTodoTask(TodoTask todoTask)
        {
            // Check for valid model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TodoTasks.Add(todoTask);
            await _context.SaveChangesAsync();

            // Return a 201 CreatedAtAction response with the new task
            return CreatedAtAction(nameof(GetTodoTaskById), new { id = todoTask.Id }, todoTask);
        }

        // Fetch By Id Operation
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetTodoTaskById(int id)
        {
            // Find the task by its ID
            var todoTask = await _context.TodoTasks.FindAsync(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }

        // Fetch All Operation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTodoTasks()
        {
            // Return all tasks from the database
            return await _context.TodoTasks.ToListAsync();
        }

        // Update Operation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoTask(int id, TodoTask todoTask)
        {
            // Ensure the ID in the route matches the ID of the object
            if (id != todoTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the task still exists before throwing an error
                if (!TodoTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete Operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoTask(int id)
        {
            // Find the task to be deleted
            var todoTask = await _context.TodoTasks.FindAsync(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            _context.TodoTasks.Remove(todoTask);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If the task was deleted by another process, we'll return NoContent anyway
                // since the desired state (task is deleted) has been achieved.
                return NoContent();
            }

            return NoContent();
        }

        private bool TodoTaskExists(int id)
        {
            return _context.TodoTasks.Any(e => e.Id == id);
        }
    }
}
