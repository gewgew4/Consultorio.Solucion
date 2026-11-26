using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;
using Consultorio.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Infrastructure.Repositorios;

public sealed class PacienteRepositorio(ConsultorioDbContext db) : IPacienteRepositorio
{
    public async Task<IReadOnlyList<Paciente>> ListarActivosAsync(CancellationToken ct = default) =>
        await db.Pacientes.AsNoTracking().Where(p => p.Activo).OrderBy(p => p.Nombre).ToListAsync(ct);

    public Task<Paciente?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        db.Pacientes.FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<Paciente?> ObtenerPorDniAsync(string dni, CancellationToken ct = default) =>
        db.Pacientes.FirstOrDefaultAsync(p => p.Dni == dni, ct);

    public Task<bool> ExisteDniAsync(string dni, CancellationToken ct = default) =>
        db.Pacientes.AnyAsync(p => p.Dni == dni, ct);

    public void Agregar(Paciente paciente) => db.Pacientes.Add(paciente);
}
