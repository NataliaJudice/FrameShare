using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Interfaces
{
    public interface IUploadService
    {
        Task<string> Upload(IFormFile file);
    }
}
