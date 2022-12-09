namespace Kinda.CodeAnalysis
{
    public enum SyntaxCategory 
    {
        // Tokens
        EndOfFileToken,
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        TimesToken,
        DivideToken,
        UnknownToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        // Expressions
        NmuberExpression,
        BinaryExpression,
        ParenthesizedExpression,
    }
}
