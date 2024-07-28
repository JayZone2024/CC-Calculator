namespace CalculatorService.Library.Functions;

public interface ICalculatorFunction
{
    char FunctionType { get; }

    decimal ExecuteFunction(decimal firstValue, decimal secondValue);
}