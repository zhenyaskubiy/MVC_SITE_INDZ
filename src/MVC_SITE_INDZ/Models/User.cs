namespace MVC_SITE_INDZ.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<ToDoTask> Tasks { get; set; }
    }
}
