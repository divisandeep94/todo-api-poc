using System;
namespace Todo_Api_Poc.Models
{
    public class TodoItem
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
    }
}

