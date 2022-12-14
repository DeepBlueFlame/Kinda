namespace Kinda.CodeAnalysis
{
    public sealed class SyntaxTree 
    {
        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken eofToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EOFToken = eofToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public ExpressionSyntax Root { get; }
        public SyntaxToken EOFToken { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}
