namespace CalculatorService.Library.Extensions;

public static class StringExtensions
{
    public static bool IsStringNumeric(this string value, out double valueAsDouble) 
        => double.TryParse(value, out valueAsDouble);

    public static decimal ToDecimal(this string value) => decimal.Parse(value);

    public static char ToChar(this string value) => char.Parse(value);
}

