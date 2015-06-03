using System.ComponentModel;

namespace Igloo.Tools
{
    public class Property
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Property(string type, string name)
        {
            Type = type;
            Name = name;
        }

        public Property()
        {
        }
    }

    public class PropertyCollection : BindingList<Property>
    {
        public new void Add(Property property)
        {
            if (!string.IsNullOrEmpty(property.Name) && !string.IsNullOrEmpty(property.Type))
            {
                base.Add(property);
            }
        }
    }
}
