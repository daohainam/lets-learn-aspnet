﻿namespace Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;    
        public bool IsCompleted { get; set; }
    }
}
