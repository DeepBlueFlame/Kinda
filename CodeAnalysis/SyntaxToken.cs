namespace Kinda.CodeAnalysis
{
    /*
    The token read from the given text
    */
    class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxCategory category, int position, string text, object value) {
            Category = category;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxCategory Category { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> get_children() {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
