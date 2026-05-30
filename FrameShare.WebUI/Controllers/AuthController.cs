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

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // Rota usada pelo painel tradicional (ex: Admin acessando via slug master)
        [HttpPost]
        public async Task<IActionResult> Login(string loginDigitado)
        {
            var usuario = await _usuarioService.BuscarPorSlug(loginDigitado.ToLower().Trim());

            if (usuario == null)
            {
                TempData["Error"] = "Acesso inválido.";
                return RedirectToAction("Login");
            }

            GerarCookieJwt(usuario.NomeCompleto, usuario.Id.ToString(), usuario.Role);

            if (usuario.Role == "Admin")
                return RedirectToAction("AdminIndex", "DriveTest");

            return RedirectToAction("Index", "DriveTest");
        }

        [HttpGet]
        public async Task<IActionResult> AutoLogin()
        {
            return View();
        }

        // ROTA 1: Executada APENAS no primeiro acesso do celular (Submissão do Form)
        [HttpPost]
        public async Task<IActionResult> CriarConvidado(string nomeCompleto)
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
                return BadRequest(new { mensagem = "Por favor, insira o seu nome." });

            try
            {
                // Cria o convidado de forma definitiva no banco de dados
                var novoUsuario = await _usuarioService.CriarConvidado(nomeCompleto);

                // Emite o Cookie JWT
                GerarCookieJwt(novoUsuario.NomeCompleto, novoUsuario.Id.ToString(), novoUsuario.Role);

                return Ok(new
                {
                    mensagem = "Acesso liberado!",
                    usuarioId = novoUsuario.Id,
                    nome = novoUsuario.NomeCompleto
                });
            }
            catch (Exception ex)
            {
                var erroReal = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { mensagem = "Erro ao registrar convidado.", erro = erroReal });
            }
        }

        // ROTA 2: Executada quando o celular já possui LocalStorage (Pula a criação e só gera o JWT)
        [HttpPost]
        public async Task<IActionResult> RenovarSessao(int usuarioId, string nome)
        {
            if (usuarioId <= 0 || string.IsNullOrWhiteSpace(nome))
                return BadRequest(new { mensagem = "Credenciais locais inválidas." });

            try
            {
                // Apenas gera o cookie de autenticação com os dados que o celular já possui guardados
                // Evita qualquer overhead ou validação complexa de insert no banco
                GerarCookieJwt(nome.Trim(), usuarioId.ToString(), "Convidado");

                return Ok(new { mensagem = "Bem-vindo de volta!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao renovar sessão do evento.", erro = ex.Message });
            }
        }

        private void GerarCookieJwt(string nome, string id, string role)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sua_Chave_Super_Secreta_De_32_Caracteres!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "FrameShare",
                audience: "FrameShareUsers",
                claims: claims,
                expires: DateTime.Now.AddDays(6),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("X-Access-Token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(1)
            });
        }
    }
}