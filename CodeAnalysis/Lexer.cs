namespace Kinda.CodeAnalysis
{
    /*
    The component for reading 'words' from a given text
    accroding to the defined syntax
    */
    internal sealed class Lexer
    {
        private readonly string _text; // The text
        private int _position_index;   // The position index of the current token
        private List<string> _diagnostics = new List<string>(); // For error msgs

        public Lexer(string text) {
            _text = text;
        }

        // Return the diagnostics msgs
        public IEnumerable<string> Diagnostics => _diagnostics;

        // The method for getting the current token from the string
        private char GetCurrentToken{
            get {
                // If the index is greater than the given string length
                // then return '\0'
                if (_position_index >= _text.Length)
                    return '\0';

                // Else return the token (char) at the position index
                return _text[_position_index];
            }
        }

        // Move the position index to point to the next token
        // private void move_to_next_token() {
        //     _position_index++;
        // }

        public SyntaxToken Lex() {
            // <number>: 1, 2, 3, ...
            // <operator>: +, -, * , /
            // <whitespace>: ' '

            // For the end of the text
            if (_position_index >= _text.Length) {
                return new SyntaxToken(SyntaxCategory.EndOfFileToken, _position_index, "\0", null);
            }

            // For digits in the text
            if (char.IsDigit(GetCurrentToken)) {
                var start_index = _position_index;

                // Keep scanning until the char is not a digit
                while (char.IsDigit(GetCurrentToken))
                    _position_index++;
                    // move_to_next_token();

                // Get the length of the token 
                // Also, get the substring indexing from the given string
                var token_length = _position_index - start_index;
                var token_string = _text.Substring(start_index, token_length);

                // Check the value in the token
                if (!int.TryParse(token_string, out var value))
                    _diagnostics.Add($"The number {_text} cannot be represented by an Int32.");

                // Convert the string to the int type and return 
                int.TryParse(token_string, out var num_value);
                return new SyntaxToken(SyntaxCategory.NumberToken, start_index, token_string, num_value);
            }

            // For white space in the text
            if (char.IsWhiteSpace(GetCurrentToken)) {
                 var start_index = _position_index;

                // Keep scanning until the char is not a whitespace
                while (char.IsWhiteSpace(GetCurrentToken))
                    _position_index++;
                    // move_to_next_token();

                // Get the length of the token 
                // Also, get the substring indexing from the given string
                var token_length = _position_index - start_index;
                var token_string = _text.Substring(start_index, token_length);

                return new SyntaxToken(SyntaxCategory.WhiteSpaceToken, start_index, token_string, null);
            }

            // For operators
            // if (GetCurrentToken == '+') {
            //     return new SyntaxToken(SyntaxCategory.PlusToken, _position_index++, "+", null);
            // } else if (GetCurrentToken == '-') {
            //     return new SyntaxToken(SyntaxCategory.MinusToken, _position_index++, "-", null);
            // } else if (GetCurrentToken == '*') {
            //     return new SyntaxToken(SyntaxCategory.TimesToken, _position_index++, "*", null);
            // } else if (GetCurrentToken == '/') {
            //     return new SyntaxToken(SyntaxCategory.DivideToken, _position_index++, "/", null);
            // } else if (GetCurrentToken == '(') {
            //     return new SyntaxToken(SyntaxCategory.OpenParenthesisToken, _position_index++, "(", null);
            // } else if (GetCurrentToken == ')') {
            //     return new SyntaxToken(SyntaxCategory.CloseParenthesisToken, _position_index++, ")", null);
            // } 

            switch (GetCurrentToken)
            {
                case '+':
                    return new SyntaxToken(SyntaxCategory.PlusToken, _position_index++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxCategory.MinusToken, _position_index++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxCategory.TimesToken, _position_index++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxCategory.DivideToken, _position_index++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxCategory.OpenParenthesisToken, _position_index++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxCategory.CloseParenthesisToken, _position_index++, ")", null);
            }

            // For unknown token error exception msgs
            _diagnostics.Add($"ERROR: Unknown token: '{GetCurrentToken}'");

            // For other types of token, just return the char 
            return new SyntaxToken(SyntaxCategory.UnknownToken, _position_index++, _text.Substring(_position_index - 1, 1), null);
        }
    }
}
