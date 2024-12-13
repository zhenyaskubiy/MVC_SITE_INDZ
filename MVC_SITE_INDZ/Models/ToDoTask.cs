using System;

namespace MVC_SITE_INDZ.Models
{
    public class ToDoTask
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime Deadline { get; set; } 
        public bool IsCompleted { get; set; } 
        public string AddedByUser { get; set; } 
    }
}
