using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace RoslynCodeGenerator
{
    public class CodeGenerator
    {
        private ClassNameRewriter _rewriter;

        public string Template { get; set; }

        private SyntaxTree _tree;

        protected SyntaxTree SyntaxTree
        {
            get { return _tree; }
        }

        public CodeGenerator(string template)
        {
            Template = template;
            _tree = CSharpSyntaxTree.ParseText(Template);

            var diagnostics = _tree.GetDiagnostics();

            if (diagnostics.Count(d => d.Severity == DiagnosticSeverity.Error) > 0)
            {
                var errorReport = new StringBuilder("Provided code template has errors: ");
                foreach (var diag in diagnostics)
                {
                    errorReport.AppendFormat("Error: {0}{1}", diag.ToString(), Environment.NewLine);
                }

                throw new SyntaxErrorException(errorReport.ToString());
            }
        }

        //override
    }
}