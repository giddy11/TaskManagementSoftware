using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.UserManagement;

namespace TaskManagement.Domain.Dtos
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;
        public string? PasswordHash { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public AccountType AccountType { get; set; }
        public Gender Gender { get; set; }
    }
}
