using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using prueba.tec.auth.domain.Entity.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pruebas.tec.auth.application.Utils
{
    public class JWTUtils
    {

        private readonly IConfiguration configuration;

        public JWTUtils(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:JWTSecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("id", user.Id.ToString())
    };

            // Añadir roles directamente
            if (user.UserRoles != null)
            {
                foreach (var ur in user.UserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, ur.Role.Name));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
