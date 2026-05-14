using FrameShare.Application.Interfaces;
using FrameShare.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

[Authorize]
public class DriveTestController : Controller
{
    private readonly IUploadService _uploadService;
    private readonly IFotoService _fotoService;
    public readonly IMissaoService _missaoService;
    private int UsuarioIdLogado =>
     int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    public DriveTestController(IUploadService uploadService, IFotoService fotoService, IMissaoService missaoService)
    {
        _uploadService = uploadService;
        _fotoService = fotoService;
        _missaoService = missaoService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        int eventoId = 1;
        var fotos = await _fotoService.BuscarRecentes(eventoId, 5);
        var missoes = await _missaoService.Buscar(eventoId);

        var indexvm = new IndexViewModel
        {
            Fotos = fotos,
            Missao = missoes
        };
        return View(indexvm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)
    {
        try
        {
            string imageUrl = await _uploadService.Upload(file);
            await _fotoService.Criar(null, 1, UsuarioIdLogado, imageUrl);
            ViewBag.Message = "Upload concluído com sucesso!";
            ViewBag.ImageUrl = imageUrl;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;

            var fotos = await _fotoService.Buscar(1);
            return View(fotos);
        }   
        
    }

    public async Task<IActionResult> UploadPorMissao(IFormFile file, int idMissao)
    {
        try
        {
            string imageUrl = await _uploadService.Upload(file);
           await _fotoService.Criar(idMissao, 1, UsuarioIdLogado, imageUrl);
            ViewBag.Message = "Upload concluído com sucesso!";
            ViewBag.ImageUrl = imageUrl;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;

            var fotos = await _fotoService.Buscar(1);
            return View(fotos);
        }
      
    }

    [HttpGet]
    public async Task<IActionResult> AdminIndex()
    {
        var missoes = await _missaoService.Buscar(1);
        var viewmodel = new IndexAdminViewModel
        {
            Missao = missoes
        };
        return View(viewmodel);
    }
}