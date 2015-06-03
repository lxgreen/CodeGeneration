using System;

namespace Igloo.Tools
{
    public class PropertiesRegion : ClassCodeRegion
    {
        public PropertiesRegion(ClassGenerator generator)
            : base(ClassRegionType.Properties, generator)
        {
        }

        public override void RenderCode(System.IO.StreamWriter writer)
        {
            Templates.Clear();
            foreach (var property in Generator.Properties)
            {
                if (!string.IsNullOrEmpty(property.Type) && !string.IsNullOrEmpty(property.Name))
                {
                    Templates.Add(property.Name, new AutoPropertyTemplate(property.Type, property.Name));
                }
            }

            base.RenderCode(writer);
        }
    }

    public class AutoPropertyTemplate : Template<string, string>
    {
        private const string _body = "public <param1> <param2> { get; set; }";

        public AutoPropertyTemplate(Type type, string name)
            : base(_body, type.Name, name)
        {
        }

        public AutoPropertyTemplate(string typeName, string name)
            : base(_body, typeName, name)
        {
        }
    }
}
