namespace Consultorio.Domain.Tarifas;

public interface ICalculadorTarifa
{
    decimal Calcular(decimal valorConsultaBase, DateTime inicio);
}
