using FrameShare.Application.Interfaces;
using FrameShare.Application.Utils;
using FrameShare.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IApplicationDbContext _context;
        public UsuarioService(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<Usuario> BuscarPorSlug(string slug)
        {
            return _context.Usuario.FirstOrDefault(x => x.SlugLogin == slug);
        }

        public async Task<Usuario> CriarConvidado(string nomeCompleto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeCompleto))
                    throw new ArgumentException("O nome completo do convidado é obrigatório.");


                var novoUsuario = new Usuario
                {
                    NomeCompleto = nomeCompleto.Trim(),
                    SlugLogin = $"convidado-{Guid.NewGuid().ToString().Substring(0, 8)}",
                    Role = "Convidado",
                    EventoId = 1
                };

                _context.Usuario.Add(novoUsuario);

                // Persiste de forma assíncrona aguardando o banco responder
                await _context.SaveChangesAsync();
                return novoUsuario;
            }
            catch (ArgumentException) { throw; }
            catch (InvalidOperationException) { throw; }
            catch (Exception ex)
            {
                throw new ApplicationException("Falha interna ao tentar persistir o novo convidado no banco de dados.", ex);
            }
        }
    
}
}
