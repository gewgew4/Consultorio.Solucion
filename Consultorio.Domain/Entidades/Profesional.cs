using Consultorio.Domain.Comun;

namespace Consultorio.Domain.Entidades;

public sealed class Profesional
{
    Profesional() { }

    public int Id { get; private set; }
    public string Matrícula { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Especialidad { get; private set; } = string.Empty;
    public decimal ValorConsulta { get; private set; }
    public bool Activo { get; private set; }

    public static Result<Profesional> Crear(string matrícula, string nombre, string especialidad, decimal valorConsulta)
    {
        var mat = (matrícula ?? string.Empty).Trim();
        if (mat.Length is 0 or > 20)
            return Errores.Profesional.MatrículaObligatoria;

        var nom = (nombre ?? string.Empty).Trim();
        if (nom.Length is 0 or > 80)
            return Errores.Profesional.NombreObligatorio;

        var esp = (especialidad ?? string.Empty).Trim();
        if (esp.Length is 0 or > 60)
            return Errores.Profesional.EspecialidadObligatoria;

        if (valorConsulta <= 0)
            return Errores.Profesional.ValorConsultaInválido;

        return new Profesional
        {
            Matrícula = mat,
            Nombre = nom,
            Especialidad = esp,
            ValorConsulta = valorConsulta,
            Activo = true
        };
    }

    public void Deshabilitar() => Activo = false;
}
