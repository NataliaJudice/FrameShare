using FrameShare.Application.DTOs;
using FrameShare.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FrameShare.WebUI.Controllers
{
    public class MissaoController : Controller
    {
        private readonly IUploadService _uploadService;
        public readonly IMissaoService _missaoService;
        public MissaoController(IUploadService uploadService, IMissaoService missaoService)
        {
            _uploadService = uploadService;
            _missaoService = missaoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(MissaoDTO missaoDTO) 
        {
            if (missaoDTO == null)
                TempData["Error"] = "Informações inválidas.";
            try
            {
                await _missaoService.Criar(missaoDTO);
                TempData["Success"] = "Missão lançada com sucesso! 🚀";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro na criação: " + ex.Message;
            }

            return RedirectToAction("AdminIndex","DriveTest");
        }

        [HttpPost]
        public async Task<IActionResult> Inativar(int idMissao)
        {
            if (idMissao == 0)
                TempData["Error"] = "Nenhum Id passado";
            try
            {
                await _missaoService.Inativar(idMissao);
                TempData["Success"] = "Missão inativada com sucesso!";
            }   
            catch(Exception ex)
            {
                TempData["Error"] = "Erro ao inativar:" + ex;
            }
            return RedirectToAction("AdminIndex", "DriveTest");
        }
    }
}
