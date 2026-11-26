namespace Consultorio.Domain.Comun;

public sealed class Result<T> : Result
{
    readonly T? _valor;

    internal Result(T? valor, bool esÉxito, Error error) : base(esÉxito, error) => _valor = valor;

    public T Valor => EsÉxito
        ? _valor!
        : throw new InvalidOperationException("No se puede leer el valor de un resultado fallido.");

    public static implicit operator Result<T>(T valor) => Éxito(valor);
    public static implicit operator Result<T>(Error error) => Fallo<T>(error);
}
