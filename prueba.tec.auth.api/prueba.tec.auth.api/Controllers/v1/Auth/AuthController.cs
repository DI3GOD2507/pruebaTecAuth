using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba.tec.auth.domain.Entity.Auth;
using prueba.tec.auth.domain.Models.Request.Auth;
using pruebas.tec.auth.application.Interfaces.Auth;
using pruebas.tec.auth.application.Services.Auth;

namespace prueba.tec.auth.api.Controllers.v1.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            await authService.RegisterAsync(user, request.PasswordHash);
            return Ok(new { Message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await authService.LoginAsync(request.Email, request.PasswordHash);
            return Ok(new { Token = token });
        }

    }
}
