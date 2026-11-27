namespace Consultorio.Application.Turnos;

public sealed record TurnoDto(
    int Id,
    string MatrículaProfesional,
    string NombreProfesional,
    string DniPaciente,
    string NombrePaciente,
    DateTime Inicio,
    DateTime Fin,
    decimal Importe,
    string Estado);
