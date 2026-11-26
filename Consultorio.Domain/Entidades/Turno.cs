using Consultorio.Domain.Comun;
using Consultorio.Domain.Tarifas;

namespace Consultorio.Domain.Entidades;

public sealed class Turno
{
    public static readonly TimeSpan Duración = TimeSpan.FromMinutes(45);
    static readonly TimeSpan HoraInicioMínima = new(9, 0, 0);
    static readonly TimeSpan HoraInicioMáxima = new(20, 30, 0);
    const int DíasAnticipaciónMáxima = 10;
    static readonly TimeSpan AnticipaciónMínimaCancelación = TimeSpan.FromHours(24);

    Turno() { }

    public int Id { get; private set; }
    public int ProfesionalId { get; private set; }
    public int PacienteId { get; private set; }
    public DateTime Inicio { get; private set; }
    public DateTime Fin { get; private set; }
    public decimal Importe { get; private set; }
    public EstadoTurno Estado { get; private set; }

    public Profesional Profesional { get; private set; } = null!;
    public Paciente Paciente { get; private set; } = null!;

    public static Result<Turno> Crear(
        Profesional profesional,
        Paciente paciente,
        DateTime inicio,
        DateTime ahora,
        ICalculadorTarifa calculadorTarifa)
    {
        if (!profesional.Activo)
            return Errores.Turno.ProfesionalNoDisponible;

        if (!paciente.Activo)
            return Errores.Turno.PacienteInactivo;

        if (inicio.Second != 0 || inicio.Millisecond != 0 || (inicio.Minute != 0 && inicio.Minute != 30))
            return Errores.Turno.HorarioInválido;

        if (inicio.TimeOfDay < HoraInicioMínima || inicio.TimeOfDay > HoraInicioMáxima)
            return Errores.Turno.FueraDeHorario;

        if (inicio < ahora || inicio.Date > ahora.Date.AddDays(DíasAnticipaciónMáxima))
            return Errores.Turno.FechaFueraDeRango;

        var fin = inicio.Add(Duración);
        var importe = calculadorTarifa.Calcular(profesional.ValorConsulta, inicio);

        return new Turno
        {
            ProfesionalId = profesional.Id,
            PacienteId = paciente.Id,
            Inicio = inicio,
            Fin = fin,
            Importe = importe,
            Estado = EstadoTurno.Confirmado
        };
    }

    public Result Cancelar(DateTime ahora)
    {
        if (Estado != EstadoTurno.Confirmado)
            return Result.Fallo(Errores.Turno.YaCancelado);

        if (Inicio - ahora < AnticipaciónMínimaCancelación)
            return Result.Fallo(Errores.Turno.CancelaciónTardía);

        Estado = EstadoTurno.Cancelado;
        return Result.Éxito();
    }
}
