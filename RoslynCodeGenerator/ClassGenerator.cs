using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynCodeGenerator
{
    public class ClassGenerator : CodeGenerator
    {
        private SyntaxNode _root;

        public ClassGenerator(string template)
            : base(template)
        {
            _root = SyntaxTree.GetRoot();
        }

        public string ClassName
        {
            get
            {
                var name = string.Empty;
                var classDeclaration = _root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
                if (classDeclaration != null)
                {
                    name = classDeclaration.Identifier.Text;
                }

                return name;
            }
            set
            {
                var classDeclaration = _root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
                if (classDeclaration != null)
                {
                    var newName = SyntaxFactory.Identifier(classDeclaration.Identifier.LeadingTrivia, value, classDeclaration.Identifier.TrailingTrivia);
                    classDeclaration = classDeclaration.Update(
                        classDeclaration.AttributeLists,
                        classDeclaration.Modifiers,
                        classDeclaration.Keyword, newName,
                        classDeclaration.TypeParameterList,
                        classDeclaration.BaseList,
                        classDeclaration.ConstraintClauses,
                        classDeclaration.OpenBraceToken,
                        classDeclaration.Members,
                        classDeclaration.CloseBraceToken,
                        classDeclaration.SemicolonToken);
                }
            }
        }
    }
}
