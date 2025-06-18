using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using prueba.tec.auth.domain.Entity.Auth;
using pruebas.tec.auth.application.Interfaces.Auth;
using pruebas.tec.auth.application.Utils;
using pruebas.tec.auth.infrastructure.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pruebas.tec.auth.application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTUtils _jwtUtils;

        public AuthService(IUserRepository userRepository, JWTUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            return _jwtUtils.GenerateToken(user);
        }

        public async Task RegisterAsync(User user, string password)
        {
            bool exists = await _userRepository.ExistsByUsernameOrEmailAsync(user.Username, user.Email);
            if (exists)
                throw new InvalidOperationException("El nombre de usuario o correo ya existe.");

            user.PasswordHash = HashPassword(password);
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
