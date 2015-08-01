using System.ComponentModel;

namespace Igloo.Tools
{
    public class Property
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsAuto { get; set; }

        public Template Getter { get; set; }

        public Template Setter { get; set; }

        public Property(string type, string name) : this(type, name, true)
        {
        }

        public Property(string type, string name, bool isAuto)
        {
            IsAuto = isAuto;
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
