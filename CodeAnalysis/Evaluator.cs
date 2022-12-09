namespace Kinda.CodeAnalysis
{
    public class Evaluator
    {
        private readonly ExpressionSyntax _root;

        public Evaluator(ExpressionSyntax root) 
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        public int EvaluateExpression(ExpressionSyntax node)
        {
            // For numberexpression, return the value
            if (node is NmuberExpressionSyntax n) 
                return (int) n.NumberToken.Value;

            // For binaryexpression
            if (node is BinaryExpressionSyntax b)
            {
                // Get the end leaf node of the binary expression
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                // Evaluate the result accroding to the operator token
                if (b.OperatorToken.Category == SyntaxCategory.PlusToken)
                    return left + right;
                else if (b.OperatorToken.Category == SyntaxCategory.MinusToken)
                    return left - right;
                else if (b.OperatorToken.Category == SyntaxCategory.TimesToken)
                    return left * right;
                else if (b.OperatorToken.Category == SyntaxCategory.DivideToken)
                    return left / right;
                else
                    throw new Exception($"Unknown binary operator {b.OperatorToken.Category}");
            }

            // For Parenthesized expression
            if (node is ParenthesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);

            // For other undefined tokens, throw an exception
            throw new Exception($"Unknown node {node.Category}");
        }
    }
}
