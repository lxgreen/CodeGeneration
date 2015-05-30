using System.IO;
using System.Text;

namespace Igloo.Tools
{
    public class AttributesRegion : ClassCodeRegion
    {
        public AttributesRegion(ClassGenerator generator)
            : base(ClassRegionType.ClassAttributes, generator)
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
            if (Templates.Count > 0)
            {
                base.RenderStart(writer);
                writer.Write(Generator.CurrentIndent, "[");
            }
        }

        public override void RenderEnd(StreamWriter writer)
        {
            if (Templates.Count > 0)
            {
                base.RenderEnd(writer);
                writer.WriteLine("]");
            }
        }

        public override void RenderCode(StreamWriter writer)
        {
            var attributes = new StringBuilder();
            var comma = "";
            foreach (var template in Templates.Values)
            {
                attributes.Append(comma);
                attributes.Append(template.ToString());
                comma = ", ";
            }
            writer.Write(attributes.ToString());
        }
    }
}
