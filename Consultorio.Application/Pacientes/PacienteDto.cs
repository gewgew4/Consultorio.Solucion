namespace Consultorio.Application.Pacientes;

public sealed record PacienteDto(int Id, string Dni, string Nombre, bool Activo);
