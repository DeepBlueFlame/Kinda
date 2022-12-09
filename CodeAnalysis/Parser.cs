namespace Kinda.CodeAnalysis
{
    /*
    The component for structuring token from a given text
    into a tree-structure as a intermedium representation
    */
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position_index;
        private List<string> _diagnostics = new List<string>();

        public Parser(string text) {
            var lexer = new Lexer(text);          // Lexer for reading text
            var tokens = new List<SyntaxToken>(); // A list of tokens
            SyntaxToken token;                    // The current token

            do 
            {
                token = lexer.Lex();   // Get the next token 

                // Ignore the whitespace and the unknown tokens
                // Add the token to the token list
                if (token.Category != SyntaxCategory.WhiteSpaceToken &&
                    token.Category != SyntaxCategory.UnknownToken) 
                        tokens.Add(token);

            } while (token.Category != SyntaxCategory.EndOfFileToken);

            _tokens = tokens.ToArray(); // Convert the list to array
            _diagnostics.AddRange(lexer.Diagnostics); // Collect the diagnostics report from the lexer
        }

        // return the diagnostics information
        public IEnumerable<string> Diagnostics => _diagnostics;

        // Return a token offset from the current position index
        private SyntaxToken Peek(int offset) 
        {
            // Compute the index of the token to return
            var index = _position_index + offset;

            // If the index is greater than the length of the tokens list
            // Then return the final token in the token list
            if (index > _tokens.Length)
                return _tokens[_position_index - 1];
            
            return _tokens[index];
        }

        // Return the  token in the token list
        // Use => for lambda expression
        public SyntaxToken GetCurrentToken => Peek(0);

        // Build the syntax tree with expression and the EOF token
        public SyntaxTree Parse() 
        {
            var expression = ParseExpression();
            var eofToken = MatchToken(SyntaxCategory.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expression, eofToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        // The implementation of Recursive descent parser
        public ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            // If the token is operator, then return the BinaryExpressionSyntax
            // Use while for deduplicating 
            while (GetCurrentToken.Category == SyntaxCategory.PlusToken ||
                   GetCurrentToken.Category == SyntaxCategory.MinusToken) 
                {
                    var operatorToken = GetCurrentToken; // Get the operator
                    _position_index++; // Move to the next position index

                    var right = ParseFactor();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
                }

            // Else return the NmuberExpressionSyntax
            return left;
        }

        // The implementation of Recursive descent parser
        public ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            // If the token is operator, then return the BinaryExpressionSyntax
            // Use while for deduplicating 
            while (GetCurrentToken.Category == SyntaxCategory.TimesToken ||
                   GetCurrentToken.Category == SyntaxCategory.DivideToken) 
                {
                    var operatorToken = GetCurrentToken; // Get the operator
                    _position_index++; // Move to the next position index

                    var right = ParsePrimaryExpression();
                    left = new BinaryExpressionSyntax(left, operatorToken, right);
                }

            // Else return the NmuberExpressionSyntax
            return left;
        }

        // The implementation of the ParsePrimaryExpression()
        private ExpressionSyntax ParsePrimaryExpression() 
        {
            if (GetCurrentToken.Category == SyntaxCategory.OpenParenthesisToken)
            {
                var left = GetCurrentToken;
                _position_index++;
                var expression = ParseExpression();
                var right = MatchToken(SyntaxCategory.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            // Match the token with the NumberToken category
            var numberToken = MatchToken(SyntaxCategory.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }

        // Use for matching a specific token category
        private SyntaxToken MatchToken(SyntaxCategory category) 
        {
            var token = GetCurrentToken;
            // If match a category, then return the token
            if (token.Category == category) {
                _position_index++;
                return token;
            }

            // Give error for unknown tokens category
            _diagnostics.Add($"ERROR: unknown tokens category: <{GetCurrentToken.Category}>, Expected <{category}>");

            // If not match, then return a manufactured SyntanToken
            return new SyntaxToken(category, token.Position, null, null);
        }
    }
}
