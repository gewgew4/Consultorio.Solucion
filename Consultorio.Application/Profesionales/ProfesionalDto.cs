namespace Consultorio.Application.Profesionales;

public sealed record ProfesionalDto(int Id, string Matrícula, string Nombre, string Especialidad, decimal ValorConsulta, bool Activo);
