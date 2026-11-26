namespace Consultorio.Domain.Comun;

public class Result
{
    protected Result(bool esÉxito, Error error)
    {
        if (esÉxito && error != Error.Ninguno)
            throw new InvalidOperationException("Un resultado exitoso no puede contener un error.");
        if (!esÉxito && error == Error.Ninguno)
            throw new InvalidOperationException("Un resultado fallido debe contener un error.");

        EsÉxito = esÉxito;
        Error = error;
    }

    public bool EsÉxito { get; }
    public bool EsFallo => !EsÉxito;
    public Error Error { get; }

    public static Result Éxito() => new(true, Error.Ninguno);
    public static Result Fallo(Error error) => new(false, error);

    public static Result<T> Éxito<T>(T valor) => new(valor, true, Error.Ninguno);
    public static Result<T> Fallo<T>(Error error) => new(default, false, error);
}
