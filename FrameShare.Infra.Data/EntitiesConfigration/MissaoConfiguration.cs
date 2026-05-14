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
    public class MissaoConfiguration : IEntityTypeConfiguration<Missao>
    {
        public void Configure(EntityTypeBuilder<Missao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Descricao).HasMaxLength(256).IsRequired();
            builder.HasOne(x => x.Evento).WithMany(e => e.Missoes).HasForeignKey(x => x.EventId);


        }
    }
}
