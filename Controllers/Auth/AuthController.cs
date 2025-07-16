using BackendCafe.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendCafe.Data;
using System.Threading.Tasks;

namespace BackendCafe.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login loginData)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == loginData.Correo && u.Contrasena == loginData.Contrasena);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            return Ok(new
            {
                mensaje = "Login exitoso",
                usuario = new
                {
                    usuario.Id,
                    usuario.Nombre,
                    usuario.Correo
                }
            });
        }
    }
}
