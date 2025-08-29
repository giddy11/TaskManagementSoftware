using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public AccountType AccountType { get; set; }
    }
}
