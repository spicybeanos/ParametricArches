using JetBrains.Annotations;
using System;
using UnityEngine;

public class Evaluator : ExpressionVisitor<float>
{
    float executeMathFunction(TokenType function,float value)
    {
        switch (function)
        {
            case TokenType.Abs: return Mathf.Abs(value);
            case TokenType.Round: return Mathf.Round(value);
            case TokenType.RoundUp: return Mathf.Round(value)+1;
            case TokenType.RoundDown: return (int)value;
            case TokenType.Sin: return Mathf.Sin(value);
            case TokenType.ASin: 
                {
                    if(Mathf.Abs(value) > 1)
                    {
                        if(value > 0)
                        {
                            value = value - (int)value;
                        }
                        else
                        {
                            value = value + (int)value;
                        }
                    }
                    return Mathf.Asin(value);
                } 
            case TokenType.Cos: return Mathf.Cos(value);
            case TokenType.ACos:
                {
                    if (Mathf.Abs(value) > 1)
                    {
                        if (value > 0)
                        {
                            value = value - (int)value;
                        }
                        else
                        {
                            value = value + (int)value;
                        }
                    }
                    return Mathf.Acos(value);
                };
            case TokenType.Tan: return Mathf.Tan(value);
            case TokenType.ATan: return Mathf.Atan(value);
            case TokenType.Log10: return value > 0 ? Mathf.Log10(value) : 0;
            case TokenType.LogE: return value > 0 ? Mathf.Log(value) : 0;
            case TokenType.Log2: return value > 0 ? Mathf.Log(value,2) : 0;
            case TokenType.Sqrt: return Mathf.Sqrt(Mathf.Abs(value));
            case TokenType.Cbrt: return MathF.Cbrt(value);
            default: throw new Exception($"Not a maths function! '{function}'"); ;
        }
    }
    float t;

    public float VisitBinary(Expression.BinaryExpression expr)
    {
        float left = Evaluate(expr.Left,t);
        float right = Evaluate(expr.Right, t);

        switch (expr.Operator)
        {
            case TokenType.Greater:
                return left > right ? 1 : 0;
            case TokenType.Lesser:
                return left < right ? 1 : 0;
            case TokenType.Minus:
                return left - right;
            case TokenType.Plus:
                return left + right;
            case TokenType.Divide:
                return left / right;
            case TokenType.Multiply:
                return left * right;
            case TokenType.Modulus: 
                float q = left / right;
                float r = (q - (int)q) * right;
                return r;
            case TokenType.Power: 
                return Mathf.Pow(left, right);
            //unreachable
            default:
                throw new System.Exception($"not a valid operator!");
        }
    }

    public float VisitGroup(Expression.GroupingExpression expr)
    {
        return Evaluate(expr.expression, t);
    }

    public float VisitLiteral(Expression.LiteralExpresion expr)
    {
        return expr.Value;
    }

    public float VisitUnary(Expression.UnaryExpression expr)
    {
        float right = Evaluate(expr.expression, t);

        switch (expr.Sign)
        {
            case TokenType.Minus:
                return -right;
            default:
                return executeMathFunction(expr.Sign, right);
        }

        // Unreachable.
    }


    public float Evaluate(Expression expr, float t_i)
    {
        t = t_i;
        return expr.Accept(this);
    }

    public float VisitT(Expression.TExpression expression)
    {
        return t;
    }
}
