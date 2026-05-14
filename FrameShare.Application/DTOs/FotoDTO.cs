using FrameShare.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.DTOs
{
    public class FotoDTO
    {
        public Guid Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public int? MissionId { get; set; }
        public string UrlDrive { get; set; }
        public DateTime DataUpload { get; set; }
    }
}
