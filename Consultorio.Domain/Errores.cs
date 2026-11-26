using Consultorio.Domain.Comun;

namespace Consultorio.Domain;

public static class Errores
{
    public static class Profesional
    {
        public static readonly Error MatrículaObligatoria = new("Profesional.MatrículaObligatoria", "La matrícula es obligatoria y no puede superar los 20 caracteres.");
        public static readonly Error MatrículaDuplicada = new("Profesional.MatrículaDuplicada", "Ya existe un profesional con esa matrícula.");
        public static readonly Error NombreObligatorio = new("Profesional.NombreObligatorio", "El nombre es obligatorio y no puede superar los 80 caracteres.");
        public static readonly Error EspecialidadObligatoria = new("Profesional.EspecialidadObligatoria", "La especialidad es obligatoria y no puede superar los 60 caracteres.");
        public static readonly Error ValorConsultaInválido = new("Profesional.ValorConsultaInválido", "El valor de la consulta debe ser mayor a cero.");
        public static readonly Error NoEncontrado = new("Profesional.NoEncontrado", "No se encontró el profesional.");
        public static readonly Error TieneTurnosPendientes = new("Profesional.TieneTurnosPendientes", "No se puede deshabilitar un profesional con turnos confirmados futuros.");
    }

    public static class Paciente
    {
        public static readonly Error DniInválido = new("Paciente.DniInválido", "El DNI debe tener 7 u 8 dígitos numéricos.");
        public static readonly Error DniDuplicado = new("Paciente.DniDuplicado", "Ya existe un paciente registrado con ese DNI.");
        public static readonly Error NombreObligatorio = new("Paciente.NombreObligatorio", "El nombre es obligatorio y no puede superar los 80 caracteres.");
        public static readonly Error NoEncontrado = new("Paciente.NoEncontrado", "No se encontró el paciente.");
        public static readonly Error TieneTurnosPendientes = new("Paciente.TieneTurnosPendientes", "No se puede dar de baja a un paciente con turnos confirmados futuros.");
    }

    public static class Turno
    {
        public static readonly Error HorarioInválido = new("Turno.HorarioInválido", "El horario de inicio debe corresponder a minutos :00 o :30.");
        public static readonly Error FueraDeHorario = new("Turno.FueraDeHorario", "El horario de inicio debe estar entre las 09:00 y las 20:30.");
        public static readonly Error FechaFueraDeRango = new("Turno.FechaFueraDeRango", "No se permiten fechas pasadas ni con más de 10 días de anticipación.");
        public static readonly Error ProfesionalNoDisponible = new("Turno.ProfesionalNoDisponible", "El profesional no existe o no se encuentra activo.");
        public static readonly Error PacienteInactivo = new("Turno.PacienteInactivo", "El paciente no existe o no se encuentra activo.");
        public static readonly Error Superpuesto = new("Turno.Superpuesto", "El profesional ya tiene un turno confirmado en ese horario.");
        public static readonly Error YaCancelado = new("Turno.YaCancelado", "El turno ya se encuentra cancelado.");
        public static readonly Error CancelaciónTardía = new("Turno.CancelaciónTardía", "El turno debe cancelarse con al menos 24 horas de anticipación.");
        public static readonly Error NoEncontrado = new("Turno.NoEncontrado", "No se encontró el turno solicitado.");
    }
}
