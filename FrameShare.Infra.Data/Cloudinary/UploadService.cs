using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FrameShare.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FrameShare.Infra.Data.Cloudinary
{
    public class UploadService : IUploadService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public UploadService(IConfiguration configuration)
        {
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            var account = new Account(
                cloudName,
                apiKey,
                apiSecret
            );

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

            // 1. Lista de extensões de imagem permitidas (incluindo os formatos do iPhone)
            var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp", ".heic", ".heif" };
            var extensao = Path.GetExtension(file.FileName)?.ToLower() ?? "";

            var contentType = file.ContentType?.ToLower() ?? "";

            // 2. Validação inteligente: Aceita se a extensão estiver na lista OU se o navegador identificar como imagem
            bool ehExtensaoValida = extensoesPermitidas.Contains(extensao);
            bool ehContentTypeValido = contentType.StartsWith("image/");

            if (!ehExtensaoValida && !ehContentTypeValido)
            {
                throw new ApplicationException("Formato de arquivo inválido. Por favor, envie apenas imagens (JPG, PNG, WEBP ou HEIC do iPhone).");
            }

            try
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    DisplayName = file.FileName,
                    Folder = "FRAMESHARE_2026",
                    PublicId = Guid.NewGuid().ToString()
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    return uploadResult.SecureUrl.ToString();
                else
                    throw new Exception(uploadResult.Error.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Falha no upload ao Cloudinary: " + ex.Message);
            }
        }
    }
}