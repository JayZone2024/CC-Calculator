using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Library.Functions;

public class SubtractFunction : ICalculatorFunction
{
    public char FunctionType => '-';

    public decimal ExecuteFunction(decimal firstValue, decimal secondValue)
    {
        Expression firstValueConstant = Expression.Constant(firstValue);
        Expression secondValueConstant = Expression.Constant(secondValue);

        Expression sum = Expression.Subtract(firstValueConstant, secondValueConstant);

        var lambda = Expression.Lambda<Func<decimal>>(sum);

        var value = lambda.Compile().Invoke();

        return value;
    }
}

