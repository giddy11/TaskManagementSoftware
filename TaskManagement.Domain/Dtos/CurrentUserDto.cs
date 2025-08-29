using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Dtos
{
    public class CurrentUserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AccountType { get; set; }
    }
}
