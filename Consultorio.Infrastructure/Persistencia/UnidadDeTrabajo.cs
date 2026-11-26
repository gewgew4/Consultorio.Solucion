using Consultorio.Application.Abstracciones;

namespace Consultorio.Infrastructure.Persistencia;

public sealed class UnidadDeTrabajo(ConsultorioDbContext db) : IUnidadDeTrabajo
{
    public Task GuardarAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}
