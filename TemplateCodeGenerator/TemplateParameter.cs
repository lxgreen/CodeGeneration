using System;

namespace Igloo.Tools
{
    public class TemplateParameter<T>
    {
        private T _value;

        private Func<T> _bindAction;

        public TemplateParameter(T value)
        {
            _value = value;
        }

        public TemplateParameter(Func<T> bindAction)
        {
            _bindAction = bindAction;
        }

        public override string ToString()
        {
            Bind();
            if (_value != null)
            {
                return _value.ToString();
            }

            return null;
        }

        public static implicit operator TemplateParameter<T>(T value)
        {
            return new TemplateParameter<T>(value);
        }

        public static implicit operator TemplateParameter<T>(Func<T> bindAction)
        {
            return new TemplateParameter<T>(bindAction);
        }

        private void Bind()
        {
            if (_bindAction != null)
            {
                _value = _bindAction();
            }
        }
    }
}