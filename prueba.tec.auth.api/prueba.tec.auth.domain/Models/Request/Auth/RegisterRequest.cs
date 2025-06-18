using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.tec.auth.domain.Models.Request.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
