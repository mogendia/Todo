using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_Api.Data;
using Todo_Api.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Todo_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TodoController> _logger;

        public TodoController(AppDbContext context, ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> Get()
        {
            var result = await _context.Set<TodoTask>().ToListAsync();
            if (result == null)
            {  
                _logger.LogWarning("TodoTask List Not Found ");
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TodoTask>> GetById(int id)
        {
            var result = await _context.Set<TodoTask>().FindAsync(id);
            if (result == null)
            {
                _logger.LogWarning("Todo List Not Found #{id}", id);
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Ok(result);
        }
        [HttpPost]
        [Route("")]
        public async Task <ActionResult<TodoTask>> CreateTask(TodoTask todo) 
        {
            var result =await _context.Set<TodoTask>().AddAsync(todo);
            await _context.SaveChangesAsync();
            return Ok(todo.Id);
        }
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<TodoTask>> UpdateTask(TodoTask todo)
        {
            var result =await _context.Set<TodoTask>().FindAsync(todo.Id);
            if (result == null)
                return NotFound();
            result.Name = todo.Name;
            result.IsCompleted = todo.IsCompleted;
            await _context.SaveChangesAsync();
            return Ok(todo.Id);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteTask( int id)
        {
            var result = await _context.Set<TodoTask>().FindAsync(id);
            if (result == null)
                return NotFound();
            _context.Set<TodoTask>().Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

