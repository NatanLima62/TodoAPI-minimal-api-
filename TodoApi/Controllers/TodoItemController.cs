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
    [Route("[controller]")]
    public class TodoItemController : ControllerBase
    {
        public static List<TodoItem> TodoItems = new List<TodoItem>{};
        public static List<Todo> Todos = new List<Todo>{};
        private readonly DataContext _context;

        public TodoItemController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<TodoItem>>> CreateTodoItem(int idTodo, TodoItem todoItem)
        {
            var todo = await _context.Todos.FindAsync(idTodo);
            if (todo == null)
            {
                return BadRequest("Todo não encontrado");
            }
            todo.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoItems.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            return Ok(await _context.TodoItems.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TodoItem>>> GetById(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return BadRequest("item não encontrado");
            }
            return Ok(todoItem);
        }

        [HttpPut]
        public async Task<ActionResult<List<TodoItem>>> Update(TodoItem newTodoItem)
        {
            var todoItem = await _context.TodoItems.FindAsync(newTodoItem.Id);
            if (todoItem == null)
            {
                return BadRequest("Todo não encontrado");
            }
            todoItem.Id = newTodoItem.Id;
            todoItem.Nome = newTodoItem.Nome;
            todoItem.Finalizado = newTodoItem.Finalizado;
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoItems.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<TodoItem>>> Delete(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return BadRequest("Todo não encontrado");
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return Ok(await _context.TodoItems.ToListAsync());
        }
    }
}