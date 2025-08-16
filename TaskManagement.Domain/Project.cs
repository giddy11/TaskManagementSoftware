using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = default!;
        public string? Version { get; set; }
        public string? Version { get; set; }
        public string? Version { get; set; }
    }
}
