using Consultorio.Domain.Entidades;

namespace Consultorio.Domain.Repositorios;

public interface IProfesionalRepositorio
{
    Task<IReadOnlyList<Profesional>> ListarAsync(CancellationToken ct = default);
    Task<Profesional?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Profesional?> ObtenerPorMatrículaAsync(string matrícula, CancellationToken ct = default);
    Task<bool> ExisteMatrículaAsync(string matrícula, CancellationToken ct = default);
    void Agregar(Profesional profesional);
}
