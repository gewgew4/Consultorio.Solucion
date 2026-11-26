using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;
using Consultorio.Infrastructure.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Infrastructure.Repositorios;

public sealed class ProfesionalRepositorio(ConsultorioDbContext db) : IProfesionalRepositorio
{
    public async Task<IReadOnlyList<Profesional>> ListarAsync(CancellationToken ct = default) =>
        await db.Profesionales.AsNoTracking().OrderBy(p => p.Matrícula).ToListAsync(ct);

    public Task<Profesional?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        db.Profesionales.FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<Profesional?> ObtenerPorMatrículaAsync(string matrícula, CancellationToken ct = default) =>
        db.Profesionales.FirstOrDefaultAsync(p => p.Matrícula == matrícula, ct);

    public Task<bool> ExisteMatrículaAsync(string matrícula, CancellationToken ct = default) =>
        db.Profesionales.AnyAsync(p => p.Matrícula == matrícula, ct);

    public void Agregar(Profesional profesional) => db.Profesionales.Add(profesional);
}
