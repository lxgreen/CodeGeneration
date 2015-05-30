using System;

namespace Igloo.Tools
{
    public class PrivateFieldsRegion : ClassCodeRegion
    {
        public PrivateFieldsRegion(ClassGenerator generator)
            : base(ClassRegionType.PrivateFields, generator)
        {
        }

        public override void RenderCode(System.IO.StreamWriter writer)
        {
            Templates.Clear();
            foreach (var field in Generator.PrivateFields)
            {
                Templates.Add(field.Name, new PrivateFieldTemplate(field.Type, field.Name));
            }

            base.RenderCode(writer);
        }
    }

    public class PrivateFieldTemplate : Template<string, string>
    {
        private const string _body = "private <param1> <param2>;";

        public PrivateFieldTemplate(Type type, string name)
            : base(_body, type.Name, name.StartsWith("_") ? name : "_" + name)
        {
        }

        public PrivateFieldTemplate(string typeName, string name)
            : base(_body, typeName, name.StartsWith("_") ? name : "_" + name)
        {
        }
    }
}
