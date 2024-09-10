using System.Linq.Expressions;
using System.Text;

namespace CalculatorService.Library.ExpressionParsers;

public interface INumericValueParser
{
    bool IsNumericValue(char operand, CalculatorContext context, out decimal? value);

    void ParseValue(CalculatorContext context, decimal value);
}

public class NumericValueParser : INumericValueParser
{
    public bool IsNumericValue(char operand, CalculatorContext context, out decimal? value)
    {
        value = null;

        if (!char.IsDigit(operand))
        {
            return false;
        }

        var reader = context.ExpressionReader!;
        var sb = new StringBuilder();
        var valueAsChar = operand;

        while (true)
        {
            sb.Append(valueAsChar);

            reader.Read();
            valueAsChar = (char)reader.Peek();

            if (!char.IsDigit(valueAsChar) && valueAsChar != '.')
            {
                break;
            }
        }

        var valueAsString = sb.ToString();

        value = decimal.Parse(valueAsString);

        return true;
    }

    public void ParseValue(CalculatorContext context, decimal value)
    {
        var constantExpression = Expression.Constant(value);

        context.CalculatorExpressions.Push(constantExpression);
    }
}