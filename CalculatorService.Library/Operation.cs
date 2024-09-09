using System.Linq.Expressions;

namespace CalculatorService.Library;

public sealed class Operation
{
    private readonly string _name;
    private readonly Func<Expression, Expression, Expression> _operation;

    public static readonly Operation Addition = new(1, Expression.Add, "Add");
    public static readonly Operation Subtraction = new(1, Expression.Subtract, "Subtract");
    public static readonly Operation Multiplication = new(2, Expression.Multiply, "Multiply");
    public static readonly Operation Division = new(2, Expression.Divide, "Divide");

    public int Precedence { get; }
    
    public static Operation GetOperation(char operation)
    {
        return Operations.TryGetValue(operation, out var result) 
            ? result 
            : throw new InvalidCastException();
    }

    public static explicit operator Operation(char operation)
    {
        if (Operations.TryGetValue(operation, out var result))
        {
            return result;
        }

        throw new InvalidCastException();
    }

    public Expression Apply(Expression left, Expression right) => _operation(left, right);

    public static bool IsDefined(char operation) => Operations.ContainsKey(operation);

    private static readonly Dictionary<char, Operation> Operations = new()
    {
        { '+', Addition },
        { '-', Subtraction },
        { '*', Multiplication},
        { '/', Division }
    };

    public Operation(int precedence, Func<Expression, Expression, Expression> operation, string name)
    {
        Precedence = precedence;
        _operation = operation;
        _name = name;
    }
}