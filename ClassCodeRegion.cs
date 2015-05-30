using System.Diagnostics;
using System.IO;

namespace Igloo.Tools
{
    public class ClassCodeRegion
    {
        private ClassGenerator _generator;
        private TemplateCollection _templates;

        public TemplateCollection Templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = new TemplateCollection();
                }
                return _templates;
            }
        }

        public ClassGenerator Generator
        {
            get { return _generator; }
            set { _generator = value; }
        }

        public ClassCodeRegion(ClassRegionType classRegionType, ClassGenerator generator)
        {
            // TODO: Complete member initialization
            RegionType = classRegionType;

            _generator = generator;
        }

        public ClassRegionType RegionType { get; set; }

        public virtual void RenderEnd(StreamWriter writer)
        {
            if (Generator.RenderRegionBounds && this.RenderRegionBounds)
            {
                writer.WriteLine();
                writer.WriteLine(Generator.CurrentIndent, string.Format("#endregion {0}", RegionType.ToString()));
                writer.WriteLine();
            }
            var exitScope = Generator.Scope.Pop();

            Debug.Assert(exitScope == RegionType, "invalid scope");
        }

        public virtual void RenderCode(StreamWriter writer)
        {
            foreach (var template in Templates.Values)
            {
                writer.WriteLine(Generator.CurrentIndent, template.ToString());
                writer.WriteLine();
            }
        }

        public virtual void RenderStart(StreamWriter writer)
        {
            Generator.Scope.Push(this.RegionType);
            if (Generator.RenderRegionBounds && this.RenderRegionBounds)
            {
                writer.WriteLine(Generator.CurrentIndent, string.Format("#region {0}", RegionType.ToString()));
                writer.WriteLine();
            }
        }

        public virtual bool RenderRegionBounds
        {
            get
            {
                return true;
            }
        }
    }
}
