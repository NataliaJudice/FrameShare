    using FrameShare.Application.Interfaces;
    using FrameShare.Domain.Entity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.Intrinsics.Arm;

    namespace FrameShare.WebUI.Controllers
    {
        public class FotoController : Controller
        {
            private readonly IUploadService _uploadService;
            private readonly IFotoService _fotoService;
            public readonly IMissaoService _missaoService;
            public FotoController(IUploadService uploadService, IFotoService fotoService, IMissaoService missaoService)
            {
                _uploadService = uploadService;
                _fotoService = fotoService;
                _missaoService = missaoService;
            }
            public async Task<IActionResult> Index()
            {
                var fotosIniciais = await _fotoService.BuscarRecentes(1, 5,0);

                // 2. Monte seu ViewModel (ou use um objeto que contenha o que a View precisa)
                var model = new FrameShare.WebUI.ViewModels.IndexViewModel
                {
              
                    Fotos = fotosIniciais.ToList(),
                    Missao = (await _missaoService.Buscar(1)).ToList() // Carrega as missões também
                };
                return View(model);
            }

            public async Task<IActionResult> BuscarFotos(int idEvento, string ordem, int? missaoId, int pagina = 0)
            {
                int tamanhoPagina = 5;

                var fotos = await _fotoService.BuscarComFiltros(idEvento, pagina, tamanhoPagina, ordem, missaoId);

                // Se não houver fotos, retorna um código "No Content" para o JS saber que parou
                if (!fotos.Any()) return NoContent();

                return PartialView("_FotosGaleriaPartial", fotos);
            }

       
        }
    }
