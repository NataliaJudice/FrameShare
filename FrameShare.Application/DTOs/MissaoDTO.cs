using FrameShare.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.DTOs
{
    public class MissaoDTO
    {
        public int Id {  get; set; }
        public int EventId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
    }
}
