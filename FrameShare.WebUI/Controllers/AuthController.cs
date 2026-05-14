using FrameShare.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrameShare.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string loginDigitado)
        {
            // 1. Busca o usuário pelo slug normalizado
            var usuario = await _usuarioService.BuscarPorSlug(loginDigitado.ToLower().Trim());

            if (usuario == null)
            {
                TempData["Error"] = "Convidado não encontrado.";
                return RedirectToAction("Login");
            }

            // 2. Definir as permissões (Claims)
            var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, usuario.NomeCompleto),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Role, usuario.Role) // Aqui está o segredo
    };

            // 3. Gerar o Token JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sua_Chave_Super_Secreta_De_32_Caracteres!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "FrameShare",
                audience: "FrameShareUsers",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 4. Salvar no Cookie HttpOnly
            Response.Cookies.Append("X-Access-Token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(1)
            });

            // --- LÓGICA DE REDIRECIONAMENTO ---
            if (usuario.Role == "Admin")
            {
                return RedirectToAction("AdminIndex", "DriveTest");
            }

            return RedirectToAction("Index", "DriveTest");
        }
    }
}
