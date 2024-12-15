using System;

namespace MVC_SITE_INDZ.Models
{
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public int UserId { get; set; } // FK для користувача
        public User User { get; set; }
    }
}
