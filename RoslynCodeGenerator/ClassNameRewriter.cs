using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynCodeGenerator
{
    public class ClassNameRewriter : CSharpSyntaxRewriter
    {
        private SemanticModel _semanticModel;
        private string _name;

        public ClassNameRewriter(string name, SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
            _name = name;
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var newName = SyntaxFactory.Identifier(node.Identifier.LeadingTrivia, _name, node.Identifier.TrailingTrivia);
            var newDeclaration = node.Update(node.AttributeLists, node.Modifiers, node.Keyword, newName, node.TypeParameterList, node.BaseList, node.ConstraintClauses, node.OpenBraceToken, node.Members, node.CloseBraceToken, node.SemicolonToken);
            return newDeclaration;
        }
    }
}