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

        // TODO (Punto 3 - P1): Agregar TieneTurnosPendientes ("Profesional.TieneTurnosPendientes", ...).
    }

    public static class Paciente
    {
        public static readonly Error DniInválido = new("Paciente.DniInválido", "El DNI debe tener 7 u 8 dígitos numéricos.");
        public static readonly Error DniDuplicado = new("Paciente.DniDuplicado", "Ya existe un paciente registrado con ese DNI.");
        public static readonly Error NombreObligatorio = new("Paciente.NombreObligatorio", "El nombre es obligatorio y no puede superar los 80 caracteres.");
        public static readonly Error NoEncontrado = new("Paciente.NoEncontrado", "No se encontró el paciente.");

        // TODO (Punto 3 - PA1): Agregar TieneTurnosPendientes ("Paciente.TieneTurnosPendientes", ...).
    }

    // TODO (Punto 3): Agregar public static class Turno con los errores T2 a T10
    // (HorarioInválido, FueraDeHorario, FechaFueraDeRango, ProfesionalNoDisponible, PacienteInactivo, Superpuesto, YaCancelado, CancelaciónTardía, NoEncontrado).
}
