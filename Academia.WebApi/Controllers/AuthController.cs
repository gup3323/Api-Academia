using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Academia.WebApi.Data;
using AutenticacaoAutorizacao.Models;

namespace AutenticacaoAutorizacao.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppAcademiaContext _context;

        public AuthController(AppAcademiaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            var usuario = await _context
                .Clientes
                .FirstOrDefaultAsync(u =>
                        u.Nome == login.Username &&
                        u.Password == login.Password);

            if (usuario == null)
            {
                return NotFound("Usuário ou senha incorreto.");
            }

            var token = CreateToken(usuario);

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(Cliente cliente)
        {
            CreatePasswordHash(cliente.Password, out byte[] passwordhash, out byte[] passwordkey);
            cliente.PasswordHash = passwordhash;
            cliente.PasswordKey = passwordkey;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        private void CreatePasswordHash(string password,
                                out byte[] passwordhash,
                                out byte[] passwordkey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordkey = hmac.Key;
                passwordhash = hmac
                    .ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(Cliente cliente)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, cliente.Nome),
                new Claim(ClaimTypes.Role, cliente.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(JwtServices.SecretKey));

            var creds = new SigningCredentials(key,
                        SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 claims: claims,
                 signingCredentials: creds,
                 expires: DateTime.Now.AddMinutes(60)
                );

            var jwt = new JwtSecurityTokenHandler()
                            .WriteToken(token);

            return jwt;
        }
    }
}
