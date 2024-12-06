public interface ExpressionVisitor<R>
{
    public abstract R VisitLiteral(Expression.LiteralExpresion expr);
    public abstract R VisitGroup(Expression.GroupingExpression expr);
    public abstract R VisitBinary(Expression.BinaryExpression expr);
    public abstract R VisitUnary(Expression.UnaryExpression expr);
    public abstract R VisitT(Expression.TExpression expression);
}
