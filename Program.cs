using System;
using Kinda.CodeAnalysis;

namespace Kinda
{
    
    internal static class Program
    {
        private static void Main()
        {
            bool showTree = false;

            while (true)
            {
                // Write something to the console
                Console.Write("> ");
                var line = Console.ReadLine();

                // Check the input and Kill the program if enter a white space
                if (string.IsNullOrWhiteSpace(line)) 
                    return;

                if (line == "#showTree")
                {
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
                    continue;
                } 
                else if (line == "#cls") 
                {
                    Console.Clear();
                    continue;
                }

                // Get the syntax tree
                var syntaxTree = SyntaxTree.Parse(line);

                // Show the tree in a dark gray color if show tree
                if (showTree) {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    show_parse_tree(syntaxTree.Root);
                    Console.ResetColor();
                }

                // Show the error in a dark red color if has
                if (!syntaxTree.Diagnostics.Any()) 
                {
                    // If no error is given, then evaluate the result from the syntax tree
                    var evaluator = new Evaluator(syntaxTree.Root);
                    var result = evaluator.Evaluate();
                    Console.WriteLine(result);
                } 
                else
                { 
                    // Or show error
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostics in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostics);
                    
                    Console.ResetColor();
                }
            }
        }

        static void show_parse_tree(SyntaxNode node, string indent="", bool isLast = true) {
            var marker = isLast ? "└── " : "├── ";
            
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Category);

            if (node is SyntaxToken t && t.Value != null) {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│   ";

            // Get the last child of the tree 
            var last_child = node.get_children().LastOrDefault();

            foreach (var child in node.get_children()) {
                show_parse_tree(child, indent, child==last_child);
            }
        }
    }
}
