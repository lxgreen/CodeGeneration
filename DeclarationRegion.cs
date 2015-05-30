using System.IO;
using System.Text;

namespace Igloo.Tools
{
    public class DeclarationRegion : ClassCodeRegion
    {
        public DeclarationRegion(ClassGenerator generator)
            : base(ClassRegionType.ClassDeclaration, generator)
        {
        }

        public override void RenderCode(StreamWriter writer)
        {
            var modifiers = RenderModifiers();

            var inheritance = RenderInheritance();

            writer.WriteLine(Generator.CurrentIndent, string.Format("{0}{1} class {2}{3}",
                Generator.AccessModifier.ToString().ToLower(), modifiers, Generator.Name, inheritance));
            writer.WriteLine(Generator.CurrentIndent, "{");
        }

        private string RenderInheritance()
        {
            var inheritance = new StringBuilder("");
            var comma = "";
            var column = " : ";
            if (Generator.BaseClass != null)
            {
                inheritance.Append(column);
                column = string.Empty;
                inheritance.Append(Generator.BaseClass);
                comma = ", ";
            }

            foreach (var implementedInterface in Generator.Interfaces)
            {
                inheritance.Append(column);
                column = string.Empty;
                inheritance.Append(comma);
                inheritance.Append(implementedInterface.Name);
                comma = ", ";
            }
            return inheritance.ToString();
        }

        private string RenderModifiers()
        {
            var modifiers = new StringBuilder("");
            modifiers.Append((Generator.Modifiers & ClassModifiers.Abstract) == ClassModifiers.Abstract ? " abstract" : "");
            modifiers.Append((Generator.Modifiers & ClassModifiers.Sealed) == ClassModifiers.Sealed ? " sealed" : "");
            modifiers.Append((Generator.Modifiers & ClassModifiers.Static) == ClassModifiers.Static ? " static" : "");
            return modifiers.ToString();
        }

        public override void RenderEnd(StreamWriter writer)
        {
            writer.WriteLine(Generator.CurrentIndent, "}");
            base.RenderEnd(writer);
        }

        public override bool RenderRegionBounds
        {
            get
            {
                return false;
            }
        }
    }
}
