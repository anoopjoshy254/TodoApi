using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var todos = _context.Todos
                .Where(x => x.UserId == userId)
                .ToList();

            return Ok(todos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoDto dto)
        {
            int userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var todo = new TodoItem
            {
                Title = dto.Title,
                UserId = userId
            };

            _context.Todos.Add(todo);

            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ToggleTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
                return NotFound();

            todo.IsCompleted = !todo.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
                return NotFound();

            _context.Todos.Remove(todo);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}