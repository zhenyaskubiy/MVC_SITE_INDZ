using System;

namespace MVC_SITE_INDZ.Task
{
    public class Task
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime Deadline { get; set; } 
        public bool IsCompleted { get; set; } 
        public string AddedByUser { get; set; } 
    }
}
