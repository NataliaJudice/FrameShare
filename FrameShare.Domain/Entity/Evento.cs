using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Domain.Entity
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PastaDriveLink { get; set; }
        public DateTime DataEvento { get; set; }
        public ICollection<Missao> Missoes { get; set; }
        public ICollection<Foto> Fotos { get; set; }
  
    }
}
