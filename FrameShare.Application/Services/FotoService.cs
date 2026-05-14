using FrameShare.Application.DTOs;
using FrameShare.Application.Interfaces;
using FrameShare.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Services
{
    public class FotoService : IFotoService
    {
        private readonly IApplicationDbContext _context;
        public FotoService(IApplicationDbContext context)
        {
            _context = context;

        }
        public Task<bool> Apagar(Guid idFoto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Foto>> Buscar(int idEvento)
        {
            var result = await _context.Foto.OrderByDescending(f => f.DataUpload).ToListAsync();
            return result;

        }
        public async Task<IEnumerable<Foto>> BuscarRecentes(int idEvento, int tamanhoPagina, int pagina = 0)
        {
            return await _context.Foto
        .Where(f => f.EventId == idEvento) // Filtra pelo evento
        .OrderByDescending(f => f.DataUpload)
        .Skip(pagina * tamanhoPagina) // Pula as fotos que já foram carregadas
        .Take(tamanhoPagina)          // Pega apenas as próximas 20
        .ToListAsync();
        }

        public async Task<IEnumerable<Foto>> BuscarComFiltros(int idEvento, int pagina, int tamanho, string ordem, int? missaoId)
        {
            var query = _context.Foto.AsQueryable().Where(f => f.EventId == idEvento);
            if (missaoId.HasValue && missaoId > 0)
                query = query.Where(f => f.MissionId == missaoId);

            if (ordem == "asc")
                query = query.OrderBy(f => f.DataUpload);
            else
                query = query.OrderByDescending(f => f.DataUpload);

            return await query
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToListAsync();
        }

        public Task<bool> BuscarPorId(int idEvento, Guid idFoto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BuscarPorMissao(int idEvento, int idMissao)
        {
            throw new NotImplementedException();
        }

        public async Task Criar(int? idMissao, int idEvent, int userid, string urlDrive)
        {
            var foto = new Foto(idMissao, idEvent, userid, urlDrive);
            _context.Foto.Add(foto);
            _context.SaveChangesAsync();
            
        }

        public Task<bool> Editar(FotoDTO fotoDTO, Guid idFoto)
        {
            throw new NotImplementedException();
        }
    }
}
