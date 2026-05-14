using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Domain.Entity
{
    public class Missao
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Evento Evento { get; set; }
        public string? Titulo { get; set; }
        public string Descricao { get; set; } 
        public string Icone { get; set; }
        public bool Status {  get; set; }
        public DateTime? DataCadastro { get; set; }
        public ICollection<Foto> Fotos { get; set; }
        public Missao()
        {

        }

        public Missao(int eventId, string titulo, string descricao, string icone)
        {
            EventId = eventId;
            Titulo = titulo;
            Descricao = descricao;
            Icone = icone;
            DataCadastro = DateTime.Now;
            Status = true;
        }
    }
}
