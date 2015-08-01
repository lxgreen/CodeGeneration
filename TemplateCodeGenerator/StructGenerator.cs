using System.IO;

namespace Igloo.Tools
{
    public class StructGenerator : ClassGenerator
    {
        public override string DeclarationPattern
        {
            get
            {
                return "{0}{1} struct {2}{3}";
            }
        }
        public StructGenerator(string name, ClassAccessModifier access, StreamWriter writer) : base(name, access, writer)
        {
        }
    }
}
