namespace CalculatorService.Library.Exceptions;

public class UnknownOperandException(char operand, string message) : Exception(message)
{
    public char Operand => operand;
}