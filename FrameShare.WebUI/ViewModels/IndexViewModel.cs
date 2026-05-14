using FrameShare.Domain.Entity;

namespace FrameShare.WebUI.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Foto> Fotos { get; set; }
        public IEnumerable<Missao> Missao { get; set; }
    }
}
