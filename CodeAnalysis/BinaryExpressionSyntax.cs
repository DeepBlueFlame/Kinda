namespace Kinda.CodeAnalysis
{
    public sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right) {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public ExpressionSyntax Left { get; }
        public SyntaxToken OperatorToken { get; }
        public ExpressionSyntax Right { get; }

        public override SyntaxCategory Category => SyntaxCategory.BinaryExpression;

        public override IEnumerable<SyntaxNode> get_children()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}
