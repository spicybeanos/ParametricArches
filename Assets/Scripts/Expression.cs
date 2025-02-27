public abstract class Expression
{
    public abstract R Accept<R>(ExpressionVisitor<R> visitor);

    public class LiteralExpresion : Expression
    {
        public float Value { get; set; }

        public LiteralExpresion(float val)
        {
            Value = val;
        }

        public override R Accept<R>(ExpressionVisitor<R> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }

    public class TExpression : Expression
    {
        public TExpression()
        {
            
        }
        public override R Accept<R>(ExpressionVisitor<R> visitor)
        {
            return visitor.VisitT(this);
        }
    }

    public class UnaryExpression : Expression
    {
        public TokenType Sign { get; set; }
        public Expression expression { get; set; }

        public UnaryExpression(TokenType opp, Expression ex)
        {
            Sign = opp;
            expression = ex;
        }

        public override R Accept<R>(ExpressionVisitor<R> visitor)
        {
            return visitor.VisitUnary(this);
        }
    }

    public class BinaryExpression : Expression
    {
        public Expression Left { get; set; }
        public TokenType Operator { get; set; }
        public Token token { get; set; }
        public Expression Right { get; set; }

        public BinaryExpression(Expression left, Token opp, Expression right)
        {
            Left = left;
            Right = right;
            Operator = opp.Type;
            token = opp;
        }

        public override R Accept<R>(ExpressionVisitor<R> visitor)
        {
            return visitor.VisitBinary(this);
        }
    }

    public class GroupingExpression : Expression
    {
        public Expression expression { get; set; }

        public GroupingExpression(Expression expr)
        {
            this.expression = expr;
        }

        public override R Accept<R>(ExpressionVisitor<R> visitor)
        {
            return visitor.VisitGroup(this);
        }
    }
}