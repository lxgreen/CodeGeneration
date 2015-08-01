using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Igloo.Tools
{
    public class ClassGenerator
    {
        private StreamWriter _writer;
        private List<Type> _implementsInterfaces;
        private List<string> _imports;
        private Stack<ClassRegionType> _scope;
        private PropertyCollection _properties;

        public virtual string DeclarationPattern
        {
            get
            {
                return "{0}{1} class {2}{3}";
            }
        }

        public PropertyCollection Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new PropertyCollection();
                }
                return _properties;
            }
        }

        private PropertyCollection _fields;

        public PropertyCollection PrivateFields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new PropertyCollection();
                }
                return _fields;
            }
        }

        public UsingsRegion Usings { get; private set; }

        public AttributesRegion Attributes { get; private set; }

        internal NameSpaceRegion Namespace { get; private set; }

        public DeclarationRegion Declaration { get; private set; }

        internal PrivateFieldsRegion PrivateFieldsRegion { get; private set; }

        public CtorRegion Ctor { get; private set; }

        internal PropertiesRegion PropertyRegion { get; private set; }

        public MethodsRegion Methods { get; private set; }

        public List<Type> Interfaces
        {
            get
            {
                if (_implementsInterfaces == null)
                {
                    _implementsInterfaces = new List<Type>();
                }
                return _implementsInterfaces;
            }
        }

        public List<string> Imports
        {
            get
            {
                if (_imports == null)
                {
                    _imports = new List<string>();
                }
                return _imports;
            }
        }

        public Stack<ClassRegionType> Scope
        {
            get
            {
                if (_scope == null)
                {
                    _scope = new Stack<ClassRegionType>();
                }
                return _scope;
            }
        }

        public int CurrentIndent
        {
            get
            {
                return (_scope.Count - 1) * TabSpaces;
            }
        }

        public bool RenderRegionBounds { get; set; }

        public ClassAccessModifier AccessModifier { get; set; }

        public ClassModifiers Modifiers { get; set; }

        public virtual Template BaseClass { get; set; }

        public string NameSpace { get; set; }

        public string Name { get; set; }

        public ClassGenerator(string name, ClassAccessModifier access, StreamWriter writer)
        {
            Name = name;
            AccessModifier = access;
            _writer = writer;
            TabSpaces = 4;
            RenderRegionBounds = false;
            Modifiers = ClassModifiers.None;

            Usings = new UsingsRegion(this);
            Namespace = new NameSpaceRegion(this);
            Attributes = new AttributesRegion(this);
            Declaration = new DeclarationRegion(this);
            PrivateFieldsRegion = new PrivateFieldsRegion(this);
            Ctor = new CtorRegion(this);
            PropertyRegion = new PropertiesRegion(this);
            Methods = new MethodsRegion(this);
        }

        public void Render()
        {
            _writer.BaseStream.SetLength(0);
            _writer.BaseStream.Position = 0;

            Usings.RenderStart(_writer);
            Usings.RenderCode(_writer);
            Usings.RenderEnd(_writer);

            Namespace.RenderStart(_writer);
            Namespace.RenderCode(_writer);

            Attributes.RenderStart(_writer);
            Attributes.RenderCode(_writer);
            Attributes.RenderEnd(_writer);

            Declaration.RenderStart(_writer);
            Declaration.RenderCode(_writer);

            PrivateFieldsRegion.RenderStart(_writer);
            PrivateFieldsRegion.RenderCode(_writer);
            PrivateFieldsRegion.RenderEnd(_writer);

            Ctor.RenderStart(_writer);
            Ctor.RenderCode(_writer);
            Ctor.RenderEnd(_writer);

            PropertyRegion.RenderStart(_writer);
            PropertyRegion.RenderCode(_writer);
            PropertyRegion.RenderEnd(_writer);

            Methods.RenderStart(_writer);
            Methods.RenderCode(_writer);
            Methods.RenderEnd(_writer);

            Declaration.RenderEnd(_writer);

            Namespace.RenderEnd(_writer);

            _writer.Flush();
        }

        public override string ToString()
        {
            if (_writer.BaseStream.CanRead)
            {
                byte[] buffer = new byte[_writer.BaseStream.Length];
                _writer.BaseStream.Position = 0;
                _writer.BaseStream.Read(buffer, 0, (int)_writer.BaseStream.Length);
                return Encoding.ASCII.GetString(buffer);
            }
            else
            {
                return string.Format("{0} cannot be read. Choose another Stream", _writer.BaseStream.GetType().Name);
            }
        }

        public int TabSpaces { get; set; }
    }
}
