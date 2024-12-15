namespace MVC_SITE_INDZ.Models
{
    public class AddTaskViewModel
    {
        public Guid Id { get; set; }  
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
