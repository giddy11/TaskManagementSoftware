using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
    }
}
