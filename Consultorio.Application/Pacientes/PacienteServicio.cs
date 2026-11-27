using Consultorio.Application.Abstracciones;
using Consultorio.Domain;
using Consultorio.Domain.Comun;
using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;

namespace Consultorio.Application.Pacientes;

public sealed class PacienteServicio(
    IPacienteRepositorio repositorio,
    ITurnoRepositorio turnoRepositorio,
    IUnidadDeTrabajo unidad)
{
    public async Task<Result<IReadOnlyList<PacienteDto>>> ListarActivosAsync(CancellationToken ct = default)
    {
        var lista = await repositorio.ListarActivosAsync(ct);
        IReadOnlyList<PacienteDto> dtos = lista
            .Select(p => new PacienteDto(p.Id, p.Dni, p.Nombre, p.Activo))
            .ToList();
        return Result.Éxito(dtos);
    }

    public async Task<Result<PacienteDto>> CrearAsync(string dni, string nombre, CancellationToken ct = default)
    {
        var creado = Paciente.Crear(dni, nombre);
        if (creado.EsFallo)
            return Result.Fallo<PacienteDto>(creado.Error);

        if (await repositorio.ExisteDniAsync(creado.Valor.Dni, ct))
            return Result.Fallo<PacienteDto>(Errores.Paciente.DniDuplicado);

        repositorio.Agregar(creado.Valor);
        await unidad.GuardarAsync(ct);
        var p = creado.Valor;
        return Result.Éxito(new PacienteDto(p.Id, p.Dni, p.Nombre, p.Activo));
    }

    public async Task<Result> DarDeBajaAsync(string dni, CancellationToken ct = default)
    {
        var paciente = await repositorio.ObtenerPorDniAsync(dni.Trim(), ct);
        if (paciente is null)
            return Result.Fallo(Errores.Paciente.NoEncontrado);

        if (await turnoRepositorio.PacienteTieneTurnosFuturosAsync(paciente.Id, DateTime.Now, ct))
            return Result.Fallo(Errores.Paciente.TieneTurnosPendientes);

        paciente.Deshabilitar();
        await unidad.GuardarAsync(ct);
        return Result.Éxito();
    }
}
