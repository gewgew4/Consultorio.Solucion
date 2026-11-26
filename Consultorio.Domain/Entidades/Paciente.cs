using Consultorio.Domain.Comun;

namespace Consultorio.Domain.Entidades;

public sealed class Paciente
{
    Paciente() { }

    public int Id { get; private set; }
    public string Dni { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public bool Activo { get; private set; }

    public static Result<Paciente> Crear(string dni, string nombre)
    {
        var dniLimpio = (dni ?? string.Empty).Trim();
        if (dniLimpio.Length is < 7 or > 8 || !dniLimpio.All(char.IsDigit))
            return Errores.Paciente.DniInválido;

        var nom = (nombre ?? string.Empty).Trim();
        if (nom.Length is 0 or > 80)
            return Errores.Paciente.NombreObligatorio;

        return new Paciente
        {
            Dni = dniLimpio,
            Nombre = nom,
            Activo = true
        };
    }

    public void Deshabilitar() => Activo = false;
}
