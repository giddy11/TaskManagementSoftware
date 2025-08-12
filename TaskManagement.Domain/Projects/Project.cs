using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Projects
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public User CreatedBy { get; set; } = default!;
        public int CreatedById { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; } = ProjectStatus.Not_Started;
        //public List<TodoTask> TodoTasks { get; set; }
    }
}
