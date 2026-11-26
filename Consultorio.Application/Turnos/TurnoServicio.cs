using Consultorio.Application.Abstracciones;
using Consultorio.Domain;
using Consultorio.Domain.Comun;
using Consultorio.Domain.Entidades;
using Consultorio.Domain.Repositorios;
using Consultorio.Domain.Tarifas;

namespace Consultorio.Application.Turnos;

public sealed class TurnoServicio(
    ITurnoRepositorio turnoRepositorio,
    IProfesionalRepositorio profesionalRepositorio,
    IPacienteRepositorio pacienteRepositorio,
    ICalculadorTarifa calculadorTarifa,
    IUnidadDeTrabajo unidad)
{
    public async Task<Result<TurnoDto>> ReservarAsync(
        string matrículaProfesional,
        string dniPaciente,
        DateTime inicio,
        CancellationToken ct = default)
    {
        var profesional = await profesionalRepositorio.ObtenerPorMatrículaAsync((matrículaProfesional ?? string.Empty).Trim(), ct);
        if (profesional is null || !profesional.Activo)
            return Result.Fallo<TurnoDto>(Errores.Turno.ProfesionalNoDisponible);

        var paciente = await pacienteRepositorio.ObtenerPorDniAsync((dniPaciente ?? string.Empty).Trim(), ct);
        if (paciente is null || !paciente.Activo)
            return Result.Fallo<TurnoDto>(Errores.Turno.PacienteInactivo);

        var creado = Turno.Crear(profesional, paciente, inicio, DateTime.Now, calculadorTarifa);
        if (creado.EsFallo)
            return Result.Fallo<TurnoDto>(creado.Error);

        var turno = creado.Valor;
        if (await turnoRepositorio.ExisteSuperposiciónAsync(profesional.Id, turno.Inicio, turno.Fin, ct))
            return Result.Fallo<TurnoDto>(Errores.Turno.Superpuesto);

        turnoRepositorio.Agregar(turno);
        await unidad.GuardarAsync(ct);

        return Result.Éxito(new TurnoDto(
            turno.Id,
            profesional.Matrícula,
            profesional.Nombre,
            paciente.Dni,
            paciente.Nombre,
            turno.Inicio,
            turno.Fin,
            turno.Importe,
            turno.Estado.ToString()));
    }

    public async Task<Result> CancelarAsync(int id, CancellationToken ct = default)
    {
        var turno = await turnoRepositorio.ObtenerPorIdAsync(id, ct);
        if (turno is null)
            return Result.Fallo(Errores.Turno.NoEncontrado);

        var resultado = turno.Cancelar(DateTime.Now);
        if (resultado.EsFallo)
            return resultado;

        await unidad.GuardarAsync(ct);
        return Result.Éxito();
    }

    public async Task<Result<IReadOnlyList<TurnoDto>>> ListarAgendaDelDíaAsync(DateOnly fecha, CancellationToken ct = default)
    {
        var turnos = await turnoRepositorio.ListarAgendaDelDíaAsync(fecha, ct);
        IReadOnlyList<TurnoDto> dtos = turnos
            .Select(t => new TurnoDto(
                t.Id,
                t.Profesional.Matrícula,
                t.Profesional.Nombre,
                t.Paciente.Dni,
                t.Paciente.Nombre,
                t.Inicio,
                t.Fin,
                t.Importe,
                t.Estado.ToString()))
            .ToList();

        return Result.Éxito(dtos);
    }
}
