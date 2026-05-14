using FrameShare.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Missao> Missao { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
