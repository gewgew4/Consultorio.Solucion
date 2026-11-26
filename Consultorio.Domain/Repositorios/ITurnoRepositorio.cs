using Consultorio.Domain.Entidades;

namespace Consultorio.Domain.Repositorios;

public interface ITurnoRepositorio
{
    Task<Turno?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExisteSuperposiciónAsync(int profesionalId, DateTime inicio, DateTime fin, CancellationToken ct = default);
    Task<bool> ProfesionalTieneTurnosFuturosAsync(int profesionalId, DateTime ahora, CancellationToken ct = default);
    Task<bool> PacienteTieneTurnosFuturosAsync(int pacienteId, DateTime ahora, CancellationToken ct = default);
    Task<IReadOnlyList<Turno>> ListarAgendaDelDíaAsync(DateOnly fecha, CancellationToken ct = default);
    void Agregar(Turno turno);
}
