using Consultorio.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consultorio.Infrastructure.Persistencia.Configuraciones;

public sealed class ProfesionalConfiguracion : IEntityTypeConfiguration<Profesional>
{
    public void Configure(EntityTypeBuilder<Profesional> builder)
    {
        builder.ToTable("Profesionales");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Matrícula).HasMaxLength(20).IsRequired();
        builder.HasIndex(p => p.Matrícula).IsUnique();
        builder.Property(p => p.Nombre).HasMaxLength(80).IsRequired();
        builder.Property(p => p.Especialidad).HasMaxLength(60).IsRequired();
        builder.Property(p => p.ValorConsulta).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(p => p.Activo).IsRequired();
    }
}
