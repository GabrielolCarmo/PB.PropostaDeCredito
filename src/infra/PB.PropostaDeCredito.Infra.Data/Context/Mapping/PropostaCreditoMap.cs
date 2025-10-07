using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB.PropostaDeCredito.Domain.PropostasDeCredito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.PropostaDeCredito.Infra.Data.Context.Mapping
{
    internal class PropostaCreditoMap : IEntityTypeConfiguration<PropostaCredito>
    {
        public void Configure(EntityTypeBuilder<PropostaCredito> builder)
        {
            builder.ToTable(nameof(PropostaCredito));
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.ClienteId)
                .HasColumnName("ClienteId")
                .IsRequired();

            builder.Property(p => p.Score)
                .HasColumnName("Score")
                .IsRequired();

            builder.Property(p => p.CreditoDisponivel)
                .HasColumnName("CreditoDisponivel")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Ignore(x => x.Events);
        }
    }
}
