namespace Kinda.CodeAnalysis
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken open, ExpressionSyntax expression, SyntaxToken close)
        {
            Open = open;
            Expression = expression;
            Close = close;
        }

        public override SyntaxCategory Category => SyntaxCategory.ParenthesizedExpression;
        public SyntaxToken Open { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken Close { get; }


        public override IEnumerable<SyntaxNode> get_children()
        {
            yield return Open;
            yield return Expression;
            yield return Close;
        }
    }
}
