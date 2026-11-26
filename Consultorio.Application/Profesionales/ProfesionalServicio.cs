using Consultorio.Application.Abstracciones;
using Consultorio.Domain;
using Consultorio.Domain.Comun;
using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;

namespace Consultorio.Application.Profesionales;

// TODO (Punto 3 - P1): Inyectar ITurnoRepositorio en el constructor para verificar turnos confirmados futuros al deshabilitar.
public sealed class ProfesionalServicio(IProfesionalRepositorio repositorio, IUnidadDeTrabajo unidad)
{
    public async Task<Result<IReadOnlyList<ProfesionalDto>>> ListarAsync(CancellationToken ct = default)
    {
        var lista = await repositorio.ListarAsync(ct);
        IReadOnlyList<ProfesionalDto> dtos = lista
            .Select(p => new ProfesionalDto(p.Id, p.Matrícula, p.Nombre, p.Especialidad, p.ValorConsulta, p.Activo))
            .ToList();
        return Result.Éxito(dtos);
    }

    public async Task<Result<ProfesionalDto>> CrearAsync(string matrícula, string nombre, string especialidad, decimal valorConsulta, CancellationToken ct = default)
    {
        var creado = Profesional.Crear(matrícula, nombre, especialidad, valorConsulta);
        if (creado.EsFallo)
            return Result.Fallo<ProfesionalDto>(creado.Error);

        if (await repositorio.ExisteMatrículaAsync(creado.Valor.Matrícula, ct))
            return Result.Fallo<ProfesionalDto>(Errores.Profesional.MatrículaDuplicada);

        repositorio.Agregar(creado.Valor);
        await unidad.GuardarAsync(ct);
        var p = creado.Valor;
        return Result.Éxito(new ProfesionalDto(p.Id, p.Matrícula, p.Nombre, p.Especialidad, p.ValorConsulta, p.Activo));
    }

    public async Task<Result> DeshabilitarAsync(string matrícula, CancellationToken ct = default)
    {
        var profesional = await repositorio.ObtenerPorMatrículaAsync(matrícula.Trim(), ct);
        if (profesional is null)
            return Result.Fallo(Errores.Profesional.NoEncontrado);

        // TODO (Punto 3 - P1): Inyectar ITurnoRepositorio y validar que el profesional no tenga turnos confirmados futuros (Errores.Profesional.TieneTurnosPendientes) antes de deshabilitar.

        profesional.Deshabilitar();
        await unidad.GuardarAsync(ct);
        return Result.Éxito();
    }
}
