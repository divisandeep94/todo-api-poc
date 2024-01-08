using System;
using Todo_Api_Poc.Models;

namespace Todo_Api_Poc.Services
{
    public interface ITodoService
    {
        TodoItem GetTodo(string id);

        List<TodoItem> FetchTodoItems();

        void UpdateTodo(TodoItem todo);

        void DeleteTodo(TodoItem todo);

    }
}

