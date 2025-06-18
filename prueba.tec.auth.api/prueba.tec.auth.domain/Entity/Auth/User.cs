using prueba.tec.auth.domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.tec.auth.domain.Entity.Auth
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = null;
        public string? Email { get; set; } = null;
        public string PasswordHash { get; set; } = null;
        public List<UserRole>? UserRoles { get; set; }

    }
}
