using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FrameShare.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FrameShare.Infra.Data.Cloudinary
{
    public class UploadService : IUploadService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public UploadService()
        {
            var account = new Account(
                "drnmpymxz",
                "433262721223617",
                "eAjYncrPUX8W2d3AtMRfUg7oM_U"
            );

            _cloudinary = new CloudinaryDotNet.Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

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
            catch (Exception ex)
            {
                // Repassa o erro para o Controller tratar na UI
                throw new Exception("Falha no upload ao Cloudinary: " + ex.Message);
            }
        }
    }
}