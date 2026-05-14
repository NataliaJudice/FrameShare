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
    public class MissaoService : IMissaoService
    {
        private readonly IApplicationDbContext _context;
        public MissaoService(IApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Missao>> Buscar(int idEvento)
        {
            var missoes = await _context.Missao.Where(m => m.EventId == idEvento && m.Status).ToListAsync();
            return missoes;
        }

        public async Task<bool> BuscarPorId(int idEvento, int idMissao)
        {
            throw new NotImplementedException();
        }

        public async Task Criar(MissaoDTO missaoDTO)
        {
            try
            {
                var missao = new Missao(1, missaoDTO.Titulo, missaoDTO.Descricao, "");
                await _context.Missao.AddAsync(missao);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro:" + ex);
            }
        }

        public async Task<bool> Editar(MissaoDTO missaoDTO, int idMissao)
        {
            throw new NotImplementedException();
        }

        public async Task Inativar(int idMissao)
        {
            var missao = await _context.Missao.FirstOrDefaultAsync(x => x.Id == idMissao && x.Status);

            if (missao == null)
                throw new Exception("Missão não encontrada");
            else
            {
                missao.Status = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
