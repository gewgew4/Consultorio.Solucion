using Consultorio.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consultorio.Infrastructure.Persistencia.Configuraciones;

public sealed class PacienteConfiguracion : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Pacientes");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Dni).HasMaxLength(8).IsRequired();
        builder.HasIndex(p => p.Dni).IsUnique();
        builder.Property(p => p.Nombre).HasMaxLength(80).IsRequired();
        builder.Property(p => p.Activo).IsRequired();
    }
}
