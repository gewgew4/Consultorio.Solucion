using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;
using Consultorio.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Infrastructure.Repositorios;

public sealed class TurnoRepositorio(ConsultorioDbContext db) : ITurnoRepositorio
{
    public Task<Turno?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        db.Turnos.FirstOrDefaultAsync(t => t.Id == id, ct);

    public Task<bool> ExisteSuperposiciónAsync(int profesionalId, DateTime inicio, DateTime fin, CancellationToken ct = default) =>
        db.Turnos.AnyAsync(
            t => t.ProfesionalId == profesionalId
                 && t.Estado == EstadoTurno.Confirmado
                 && t.Inicio < fin
                 && t.Fin > inicio,
            ct);

    public Task<bool> ProfesionalTieneTurnosFuturosAsync(int profesionalId, DateTime ahora, CancellationToken ct = default) =>
        db.Turnos.AnyAsync(
            t => t.ProfesionalId == profesionalId
                 && t.Estado == EstadoTurno.Confirmado
                 && t.Inicio > ahora,
            ct);

    public Task<bool> PacienteTieneTurnosFuturosAsync(int pacienteId, DateTime ahora, CancellationToken ct = default) =>
        db.Turnos.AnyAsync(
            t => t.PacienteId == pacienteId
                 && t.Estado == EstadoTurno.Confirmado
                 && t.Inicio > ahora,
            ct);

    public async Task<IReadOnlyList<Turno>> ListarAgendaDelDíaAsync(DateOnly fecha, CancellationToken ct = default)
    {
        var desde = fecha.ToDateTime(TimeOnly.MinValue);
        var hasta = desde.AddDays(1);

        return await db.Turnos
            .AsNoTracking()
            .Include(t => t.Profesional)
            .Include(t => t.Paciente)
            .Where(t => t.Inicio >= desde && t.Inicio < hasta)
            .OrderBy(t => t.Profesional.Nombre)
            .ThenBy(t => t.Inicio)
            .ToListAsync(ct);
    }

    public void Agregar(Turno turno) => db.Turnos.Add(turno);
}
