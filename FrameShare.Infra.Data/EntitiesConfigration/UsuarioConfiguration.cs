using FrameShare.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameShare.Infra.Data.EntitiesConfigration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.NomeCompleto)
                  .IsRequired()
                  .HasMaxLength(150);

            // CRITICAL: Criar um índice para busca rápida no login
            builder.HasIndex(e => e.SlugLogin)
                  .IsUnique();

            builder.Property(e => e.SlugLogin)
                  .IsRequired()
                  .HasMaxLength(100);
        }
    }
}
