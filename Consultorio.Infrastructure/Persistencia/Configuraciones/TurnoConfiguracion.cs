using Consultorio.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consultorio.Infrastructure.Persistencia.Configuraciones;

public sealed class TurnoConfiguracion : IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.ToTable("Turnos");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.ProfesionalId).IsRequired();
        builder.Property(t => t.PacienteId).IsRequired();
        builder.Property(t => t.Inicio).HasColumnType("datetime2").IsRequired();
        builder.Property(t => t.Fin).HasColumnType("datetime2").IsRequired();
        builder.Property(t => t.Importe).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(t => t.Estado).HasConversion<string>().HasMaxLength(20).IsRequired();

        builder.HasOne(t => t.Profesional)
            .WithMany()
            .HasForeignKey(t => t.ProfesionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Paciente)
            .WithMany()
            .HasForeignKey(t => t.PacienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
