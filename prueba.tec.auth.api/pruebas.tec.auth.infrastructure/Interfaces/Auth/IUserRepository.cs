using prueba.tec.auth.domain.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruebas.tec.auth.infrastructure.Interfaces.Auth
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<bool> ExistsByUsernameOrEmailAsync(string username, string email);
        Task SaveChangesAsync();
    }
}
