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
                    if (property.IsAuto)
                    {
                        Templates.Add(property.Name, new AutoPropertyTemplate(property.Type, property.Name));
                    }
                    else
                    {
                        Templates.Add(property.Name, new PropertyTemplate(property.Type, property.Name, property.Getter, property.Setter));
                    }
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

    public class PropertyTemplate : Template<string, string, Template, Template>
    {
        private const string _body = "public <param1> <param2><b><nl>get<nl>{<nl><t><param3><nl>}<nl>set<nl>{<nl><t><param4><nl>}</b>";

        public PropertyTemplate(string type, string name, Template getter, Template setter) : base(_body, type, name, getter, setter)
        {
        }
    }
}
