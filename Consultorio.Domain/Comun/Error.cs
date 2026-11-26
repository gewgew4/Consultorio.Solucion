namespace Consultorio.Domain.Comun;

public sealed record Error(string Código, string Mensaje)
{
    public static readonly Error Ninguno = new(string.Empty, string.Empty);
}
