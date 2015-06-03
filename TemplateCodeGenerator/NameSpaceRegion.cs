using System.IO;

namespace Igloo.Tools
{
    public class NameSpaceRegion : ClassCodeRegion
    {
        public NameSpaceRegion(ClassGenerator generator)
            : base(ClassRegionType.NameSpace, generator)
        {
        }

        public override bool RenderRegionBounds
        {
            get
            {
                return false;
            }
        }

        public override void RenderStart(StreamWriter writer)
        {
            if (!string.IsNullOrEmpty(Generator.NameSpace))
            {
                base.RenderStart(writer);
            }
        }

        public override void RenderCode(StreamWriter writer)
        {
            if (!string.IsNullOrEmpty(Generator.NameSpace))
            {
                writer.WriteLine(Generator.CurrentIndent, string.Format("namespace {0}", Generator.NameSpace));
                writer.WriteLine(Generator.CurrentIndent, "{");
            }
        }

        public override void RenderEnd(StreamWriter writer)
        {
            if (!string.IsNullOrEmpty(Generator.NameSpace))
            {
                writer.WriteLine(Generator.CurrentIndent, "}");
                base.RenderEnd(writer);
            }
        }
    }
}
