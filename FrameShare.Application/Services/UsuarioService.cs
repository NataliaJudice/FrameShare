using FrameShare.Application.Interfaces;
using FrameShare.Domain.Entity;
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
    }
}
