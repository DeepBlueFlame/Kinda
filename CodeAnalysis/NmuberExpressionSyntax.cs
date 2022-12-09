namespace Kinda.CodeAnalysis
{

    sealed class NmuberExpressionSyntax : ExpressionSyntax
    {
        public NmuberExpressionSyntax(SyntaxToken numberToken) {
            NumberToken = numberToken;
        }

        public override SyntaxCategory Category => SyntaxCategory.NumberToken;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<SyntaxNode> get_children()
        {
            yield return NumberToken;
        }
    }
}
