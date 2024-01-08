using System;
using Todo_Api_Poc.Models;
using Todo_Api_Poc.Services;

namespace Todo_Api_Poc.Services
{
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> TodoList;

        public TodoService()
        {
            TodoList = new List<TodoItem>
        {
            new TodoItem { Id = "bc01190e-3fd1-4627-a159-4780d74b6159", Name = "Get groceries", Status = "Active" },
            new TodoItem { Id = "5ab8a650-8640-4e2e-91e1-cd8c0426f411", Name = "Call Mother", Status = "Active" },
            new TodoItem { Id = "91aec41a-d904-42ae-aabf-c14bb382af3e", Name = "Get Dentist Appointment", Status = "Active" },
            new TodoItem { Id = "64f6a7a7-db93-4b59-b9c8-b14c077e6966", Name = "Pay Electricity Bill", Status = "Active" },
            new TodoItem { Id = "17940753-6c18-46f5-8ed8-32bb420bb275", Name = "Clean the kitchen", Status = "Active" },
            new TodoItem { Id = "a83d05e4-48eb-47a6-b80a-2adf0728116d", Name = "Arrange clothes", Status = "Active" }
        };
        }

        public TodoItem? GetTodo(string id)
        {
            return TodoList.FirstOrDefault(todo => todo.Id == id);
        }


        public List<TodoItem> FetchTodoItems()
        {
            return TodoList;
        }

        public void UpdateTodo(TodoItem selectedTodo)
        {
            var selectedTodoindex = TodoList.FindIndex(todo => todo.Id == selectedTodo.Id);
            if (selectedTodoindex == -1)
            {
                return;
            }
            else
            {
                TodoList[selectedTodoindex] = selectedTodo;
            }
        }

        public void DeleteTodo(TodoItem selectedTodo)
        {
            TodoList.Remove(selectedTodo);
        }
    }
}

