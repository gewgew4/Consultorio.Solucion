namespace Consultorio.Domain.Tarifas;

public sealed class CalculadorTarifaVespertina : ICalculadorTarifa
{
    static readonly TimeSpan HoraInicioRecargo = new(17, 30, 0);
    const decimal FactorRecargo = 1.20m;

    public decimal Calcular(decimal valorConsultaBase, DateTime inicio) =>
        inicio.TimeOfDay >= HoraInicioRecargo
            ? Math.Round(valorConsultaBase * FactorRecargo, 2, MidpointRounding.AwayFromZero)
            : valorConsultaBase;
}
