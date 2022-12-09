namespace Kinda.CodeAnalysis
{

    public sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public LiteralExpressionSyntax(SyntaxToken literalToken) {
            LiteralToken = literalToken;
        }

        public override SyntaxCategory Category => SyntaxCategory.NumberToken;
        public SyntaxToken LiteralToken { get; }

        public override IEnumerable<SyntaxNode> get_children()
        {
            yield return LiteralToken;
        }
    }
}
