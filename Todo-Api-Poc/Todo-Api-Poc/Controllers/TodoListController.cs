using System;
using Todo_Api_Poc.Models;
using Microsoft.AspNetCore.Mvc;
using Todo_Api_Poc.Services;
using Todo_Api_Poc;

namespace Todo_Api_Poc.Controllers
{
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoListController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoItems/
        [HttpGet()]
        public ActionResult GetTodoItems()
        {
            var todoList = _todoService.FetchTodoItems();
            if (!todoList.Any())
            {
                return NotFound();
            }
            return Ok(todoList);
        }

        // PUT: api/TodoItems/bc01190e-3fd1-4627-a159-4780d74b6159
        [HttpPut("{id}")]
        public ActionResult UpdateTodoItem(string id, [FromBody] TodoItem selectedTodoitem)
        {
            if (id is null)
            {
                return BadRequest();
            }
            _todoService.UpdateTodo(selectedTodoitem);
            return Ok(Constants.UpdateSuccessStatus);
        }

        // DELETE api/TodoItems/bc01190e-3fd1-4627-a159-4780d74b6159
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoItem(string id)
        {
            var todoItem = _todoService.GetTodo(id);
            if (todoItem is null)
            {
                return NotFound();
            }
            else
            {
                _todoService.DeleteTodo(todoItem);
                return Ok(Constants.RemoveSuccessStatus);
            }
        }
    }
}