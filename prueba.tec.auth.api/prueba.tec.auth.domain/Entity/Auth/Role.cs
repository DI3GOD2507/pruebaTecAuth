using prueba.tec.auth.domain.Entity.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.tec.auth.domain.Entity.Auth
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<UserRole>? UserRoles { get; set; }
    }
}
