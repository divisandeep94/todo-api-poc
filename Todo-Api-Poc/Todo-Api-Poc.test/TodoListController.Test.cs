using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo_Api_Poc.Controllers;
using Todo_Api_Poc.Models;
using Todo_Api_Poc.Services;
using Xunit;

namespace Todo_Api_Poc.Tests.Controllers
{
    public class TodoListControllerTests
    {
        public string todoItemId = Guid.NewGuid().ToString();
        [Fact]
        public void FetchTodoItems_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            mockTodoService.Setup(service => service.FetchTodoItems())
                           .Returns(new List<TodoItem> { new TodoItem { Id = todoItemId, Name = "Get groceries", Status = "Active" } });

            var controller = new TodoListController(mockTodoService.Object);

            // Act
            var result = controller.GetTodoItems();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var todoItems = Assert.IsType<List<TodoItem>>(okResult.Value);
            Assert.Single(todoItems);
        }

        [Fact]
        public void FetchTodoItems_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            mockTodoService.Setup(service => service.FetchTodoItems())
                           .Returns(new List<TodoItem>());

            var controller = new TodoListController(mockTodoService.Object);

            // Act
            var result = controller.GetTodoItems();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateTodoItem_ShouldReturnSuccessResponse_WhenRecordIsUpdated()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var selectedTodoItem = new TodoItem { Id = todoItemId, Name = "Get groceries", Status = "Active" };

            var controller = new TodoListController(mockTodoService.Object);

            // Act
            var result = controller.UpdateTodoItem(todoItemId, selectedTodoItem);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(Constants.UpdateSuccessStatus, okResult.Value);
            mockTodoService.Verify(service => service.UpdateTodo(selectedTodoItem), Times.Once);
        }

        [Fact]
        public void UpdateTodoItem_ShouldReturnBadRequest_WhenRequestIsInvalid()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var controller = new TodoListController(mockTodoService.Object);
            var selectedTodoItem = new TodoItem { Id = todoItemId, Name = "Get groceries", Status = "Active" };

            // Act
            var result = controller.UpdateTodoItem(null, selectedTodoItem);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            mockTodoService.Verify(service => service.UpdateTodo(It.IsAny<TodoItem>()), Times.Never);
        }

        [Fact]
        public void DeleteTodoItem_ShouldReturnSuccessResponse_WhenRecordIsDeleted()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var controller = new TodoListController(mockTodoService.Object);
            var todoItem = new TodoItem { Id = todoItemId, Name = "Test Todo", Status = "Active" };

            mockTodoService.Setup(service => service.GetTodo(todoItemId))
                .Returns(todoItem);

            // Act
            var result = controller.DeleteTodoItem(todoItemId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockTodoService.Verify(service => service.DeleteTodo(todoItem), Times.Once);
        }

        [Fact]
        public void DeleteTodoItem_ShouldReturnNoDataFound_WhenRecordNotFound()
        {
            // Arrange
            var mockTodoService = new Mock<ITodoService>();
            var controller = new TodoListController(mockTodoService.Object);

            mockTodoService.Setup(service => service.GetTodo("non-existent-id"))
                .Returns(null as TodoItem);

            // Act
            var result = controller.DeleteTodoItem("non-existent-id");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
