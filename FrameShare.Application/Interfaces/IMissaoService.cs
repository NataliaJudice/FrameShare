using FrameShare.Application.DTOs;
using FrameShare.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Interfaces
{
    public interface IMissaoService
    {
        Task Criar(MissaoDTO missaoDTO);
        Task<bool> Editar(MissaoDTO missaoDTO, int idMissao);
        Task Inativar(int idMissao);
        Task<IEnumerable<Missao>> Buscar(int idEvento);
        Task<bool> BuscarPorId(int idEvento, int idMissao);

    }
}
