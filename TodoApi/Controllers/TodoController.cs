using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoApi.Data;
using TodoApi.Model;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        public static List<Todo> Todos = new List<Todo>{};
        private readonly DataContext _context;

        public TodoController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Todo>>> CreateTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAll()
        {
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Todo>>> GetById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return BadRequest("Todo não encontrado");
            }
            return Ok(todo);
        }

        [HttpPut]
        public async Task<ActionResult<List<Todo>>> Update(Todo newTodo)
        {
            var todo = await _context.Todos.FindAsync(newTodo.Id);
            if (todo == null)
            {
                return BadRequest("Todo não encontrado");
            }
            todo.Nome = newTodo.Nome;
            todo.Descricao = newTodo.Descricao;
            todo.Finalizado = newTodo.Finalizado;

            await _context.SaveChangesAsync();
            return Ok(await _context.Todos.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Todo>>> Delete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return BadRequest("Todo não encontrado");
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok(await _context.Todos.ToListAsync());
        }
    }
}