using Consultorio.Domain.Entidades;

namespace Consultorio.Domain.Repositorios;

public interface IPacienteRepositorio
{
    Task<IReadOnlyList<Paciente>> ListarActivosAsync(CancellationToken ct = default);
    Task<Paciente?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<Paciente?> ObtenerPorDniAsync(string dni, CancellationToken ct = default);
    Task<bool> ExisteDniAsync(string dni, CancellationToken ct = default);
    void Agregar(Paciente paciente);
}
