public class Token
{
    public int Start { get; private set; }
    public string Lox { get; private set; }
    public TokenType Type { get; private set; }
    public Token(int start, string lox, TokenType type)
    {
        Start = start;
        Lox = lox;
        Type = type;
    }

    public override string ToString()
    {
        return $"[{Start}]'{Lox}'({Type})";
    }

    public static TokenType GetTokenType(string str)
    {
        switch (str)
        {
            case "t": return TokenType.TParam;
            case "e": return TokenType.E;
            case "pi": return TokenType.Pi;

            case "round": return TokenType.Round;
            case "roundUp": return TokenType.RoundUp;
            case "roundDown": return TokenType.RoundDown;
            case "abs": return TokenType.Abs;

            case "sin": return TokenType.Sin;
            case "cos": return TokenType.Cos;
            case "tan": return TokenType.Tan;
            case "asin": return TokenType.ASin;
            case "acos": return TokenType.ACos;
            case "atan": return TokenType.ATan;

            case "log": return TokenType.Log10;
            case "ln": return TokenType.LogE;
            case "lg2": return TokenType.Log2;

            case "sqrt": return TokenType.Sqrt;
            case "cbrt": return TokenType.Cbrt;

            default: return TokenType.Parameter;
        }
    }
}

public class FunctionalToken
{
    public static MathFunction GetFunction(string str)
    {
        switch(str)
        {
            case "abs": return MathFunction.Abs;
            case "round": return MathFunction.Round;
            case "roundUp": return MathFunction.RoundUp;
            case "roundDown": return MathFunction.RoundDown;

            case "cos": return MathFunction.Cos;
            case "sin": return MathFunction.Sin;
            case "tan": return MathFunction.Tan;

            case "acos": return MathFunction.ACos;
            case "asin": return MathFunction.ASin;
            case "atan": return MathFunction.ATan;

            case "log": return MathFunction.Log10;
            case "ln": return MathFunction.LogE;
            case "lg2": return MathFunction.Log2;

            case "sqrt": return MathFunction.Sqrt;
            case "cbrt": return MathFunction.Cbrt;

            default:
                throw new System.Exception($"Not a math function!: '{str}'");
        }
    }
}

public enum TokenType
{
    Literal,
    OpenParenth,
    CloseParenth,
    TParam,
    Parameter,
    Const,
    Function,

    Greater,    // >
    Lesser,     // <
    Plus,       // +
    Minus,      // -
    Multiply,   // *
    Divide,     // /
    Modulus,    // %
    Power,      // ^

    Pi,
    E,

    Abs,
    Round,
    RoundUp,
    RoundDown,

    Cos,
    Sin,
    Tan,

    ACos,
    ASin,
    ATan,

    Log10,
    LogE,
    Log2,

    Sqrt,
    Cbrt,

    EOF,
}

public enum MathFunction
{
    Abs,
    Round,
    RoundUp,
    RoundDown,

    Cos,
    Sin,
    Tan,

    ACos,
    ASin,
    ATan,

    Log10,
    LogE,
    Log2,

    Sqrt,
    Cbrt,
}