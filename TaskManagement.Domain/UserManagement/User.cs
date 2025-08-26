

using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.UserManagement
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = default!;
        public string? PasswordHash { get; set; }
        public string? ConfirmPassword { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public AccountType AccountType { get; set; }
        public UserStatus UserStatus { get; set; }
        public Gender Gender { get; set; }
    }


}
