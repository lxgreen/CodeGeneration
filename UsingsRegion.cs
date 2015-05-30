using System.IO;

namespace Igloo.Tools
{
    public class UsingsRegion : ClassCodeRegion
    {
        public UsingsRegion(ClassGenerator generator)
            : base(ClassRegionType.Usings, generator)
        {
        }

        public override void RenderCode(StreamWriter writer)
        {
            foreach (var imported in Generator.Imports)
            {
                writer.WriteLine(Generator.CurrentIndent, string.Format("using {0};", imported));
            }
        }

        public override void RenderStart(StreamWriter writer)
        {
            if (Generator.Imports.Count > 0)
            {
                base.RenderStart(writer);
            }
        }

        public override void RenderEnd(StreamWriter writer)
        {
            if (Generator.Imports.Count > 0)
            {
                base.RenderEnd(writer);
            }
        }
    }
}
