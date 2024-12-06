using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;

public class Parser
{
    private readonly List<Token> tokens;
    int current = 0;

    public Expression Parse()
    {
        try
        {
            return expression();
        }
        catch (System.Exception ex)
        {
            Debug.Log("Parser exception:"+ex);
            return null;
        }
    }

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    private Expression expression(  )
    {
        return compare();
    }

    private Expression compare()
    {
        Expression expr = term();

        while (match(TokenType.Greater, TokenType.Lesser))
        {
            Token op = previous();
            Expression right = term();
            expr = new Expression.BinaryExpression(expr, op, right);
        }

        return expr;
    }

    private Expression term(    )
    {
        Expression expr = mod();

        while (match(TokenType.Minus, TokenType.Plus))
        {
            Token op = previous();
            Expression right = mod();
            expr = new Expression.BinaryExpression(expr, op, right);
        }

        return expr;
    }
    
    private Expression mod( )
    {
        Expression expr = factor();

        while (match(TokenType.Modulus))
        {
            Token op = previous();
            Expression right = factor();
            expr = new Expression.BinaryExpression(expr, op, right);
        }

        return expr;
    }

    private Expression factor(  )
    {
        Expression expr = pow();

        while (match(TokenType.Divide, TokenType.Multiply))
        {
            Token op = previous();
            Expression right = pow();
            expr = new Expression.BinaryExpression(expr, op, right);
        }

        return expr;
    }

    private Expression pow()
    {
        Expression expr = unary();

        while (match(TokenType.Power))
        {
            Token op = previous();
            Expression right = unary();
            expr = new Expression.BinaryExpression(expr, op, right);
        }

        return expr;
    }

    private Expression unary()
    {
        bool m = match(TokenType.Minus,
            TokenType.Abs,
            TokenType.Round,
            TokenType.RoundDown,
            TokenType.RoundUp,
            TokenType.Sin,
            TokenType.ASin,
            TokenType.Cos,
            TokenType.ACos,
            TokenType.Tan,
            TokenType.ATan,
            TokenType.Log10,
            TokenType.LogE,
            TokenType.Log2,
            TokenType.Sqrt,
            TokenType.Cbrt
            );
        if (m)
        {
            Token opr = previous();
            Expression right = unary();
            return new Expression.UnaryExpression(opr.Type, right);
        }

        return primary();
    }

    private Expression primary()
    {

        if (match(TokenType.Literal))
        {
            var lit = float.Parse(previous().Lox);
            return new Expression.LiteralExpresion(lit);
        }

        if (match(TokenType.TParam))
        {
            return new Expression.TExpression();
        }

        if (match(TokenType.Pi))
        {
            float lit = Mathf.PI;
            return new Expression.LiteralExpresion(lit);
        }

        if (match(TokenType.E))
        {
            float lit = Mathf.Epsilon;
            return new Expression.LiteralExpresion(lit);
        }

        if (match(TokenType.OpenParenth))
        {
            Expression expr = expression();
            consume(TokenType.CloseParenth, "Expected ')' after expression.");
            return new Expression.GroupingExpression(expr);
        }

        throw error(peek(), "Expect expression.");
    }

    /// <summary>
    /// checks if current token is equal to `type`, if it is,
    /// advance and return true, if not return false and not advance
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    private bool match(params TokenType[] types)
    {
        foreach (TokenType type in types)
        {
            if (check(type))
            {
                advance();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// returns if current token is equal to `type` and if
    /// not at end
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool check(TokenType type)
    {
        if (isAtEnd())
            return false;
        return peek().Type == type;
    }

    /// <summary>
    /// checks if the next token is `type`, if it is, returns advance
    /// else throws an exception
    /// </summary>
    /// <param name="type"></param>
    /// <param name="error_message"></param>
    /// <returns></returns>
    private Token consume(TokenType type, string error_message)
    {
        if (check(type))
            return advance();

        throw error(peek(), error_message);
    }

    private Exception error(Token token, string message)
    {
        return new Exception($"error at {token.Start},'{token.Lox}':{message}");
    }

    /// <summary>
    /// returns the current token and then increments the pointer
    /// </summary>
    /// <returns></returns>
    private Token advance()
    {
        if (!isAtEnd())
            current++;
        return previous();
    }

    /// <summary>
    /// returns if the current token is EOF
    /// </summary>
    /// <returns></returns>
    private bool isAtEnd()
    {
        return peek().Type == TokenType.EOF;
    }
    /// <summary>
    /// returns the current token
    /// </summary>
    /// <returns></returns>
    private Token peek()
    {
        return tokens[current];
    }
    /// <summary>
    /// returns the previous token, ie `current - 1`
    /// </summary>
    /// <returns></returns>
    private Token previous()
    {
        return tokens[current - 1];
    }
}
