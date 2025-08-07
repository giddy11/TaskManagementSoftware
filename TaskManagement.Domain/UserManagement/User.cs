using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.UserManagement
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string? PasswordHash { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public AccountTypes AccountType { get; set; }
        public UserStatus UserStatus { get; set; } = UserStatus.Active;
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended
    }

    public enum AccountTypes
    {
        User,
        Admin
    }
}
