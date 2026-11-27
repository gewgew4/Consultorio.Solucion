using Consultorio.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Infrastructure.Persistencia;

public sealed class ConsultorioDbContext(DbContextOptions<ConsultorioDbContext> opciones) : DbContext(opciones)
{
    public DbSet<Profesional> Profesionales => Set<Profesional>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Turno> Turnos => Set<Turno>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConsultorioDbContext).Assembly);
}
