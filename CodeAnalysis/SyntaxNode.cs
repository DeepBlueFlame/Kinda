namespace Kinda.CodeAnalysis
{
    /*
    Node for syntax tree construction
    */
    abstract class SyntaxNode 
    {
        public abstract SyntaxCategory Category { get; }

        public abstract IEnumerable<SyntaxNode> get_children();
    }
}
