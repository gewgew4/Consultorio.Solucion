using Consultorio.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Infrastructure.Persistencia;

public sealed class ConsultorioDbContext(DbContextOptions<ConsultorioDbContext> opciones) : DbContext(opciones)
{
    public DbSet<Profesional> Profesionales => Set<Profesional>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();

    // TODO (Punto 4): Agregar DbSet<Turno> Turnos => Set<Turno>(); (y crear Configuraciones/TurnoConfiguracion.cs con Fluent API).

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsultorioDbContext).Assembly);
}
