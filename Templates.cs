namespace Igloo.Tools
{
    public class Template
    {
        private static Template _empty = new Template(string.Empty);

        public static int TabSpaces = 4;

        public static Template Empty
        {
            get
            {
                return _empty;
            }
        }

        public string Body { get; set; }

        public override string ToString()
        {
            var tab = new string(' ', TabSpaces);

            return Body.Replace("<b>", string.Format("\r\n{0}{0}{{", tab))
                .Replace("</b>", string.Format("\r\n{0}{0}}}", tab))
                .Replace("<nl>", string.Format("\r\n{0}{0}{0}", tab))
                .Replace("<t>", tab);
        }

        public Template(string body)
        {
            Body = body;
        }
    }

    public class Template<T1> : Template
    {
        public TemplateParameter<T1> Parameter1 { get; set; }

        public Template(string body, TemplateParameter<T1> parameter)
            : base(body)
        {
            Parameter1 = parameter;
        }

        public override string ToString()
        {
            return base.ToString()
                .Replace("<param1>", Parameter1.ToString());
        }
    }

    public class Template<T1, T2> : Template<T1>
    {
        public TemplateParameter<T2> Parameter2 { get; set; }

        public Template(string body, TemplateParameter<T1> parameter1, TemplateParameter<T2> parameter2)
            : base(body, parameter1)
        {
            Parameter2 = parameter2;
        }

        public override string ToString()
        {
            return base.ToString()
                .Replace("<param2>", Parameter2.ToString());
        }
    }

    public class Template<T1, T2, T3> : Template<T1, T2>
    {
        public TemplateParameter<T3> Parameter3 { get; set; }

        public Template(string body, TemplateParameter<T1> parameter1, TemplateParameter<T2> parameter2, TemplateParameter<T3> parameter3)
            : base(body, parameter1, parameter2)
        {
            Parameter3 = parameter3;
        }

        public override string ToString()
        {
            return base.ToString()
                .Replace("<param3>", Parameter3.ToString());
        }
    }

    public class Template<T1, T2, T3, T4> : Template<T1, T2, T3>
    {
        public TemplateParameter<T4> Parameter4 { get; set; }

        public Template(string body, TemplateParameter<T1> parameter1, TemplateParameter<T2> parameter2, TemplateParameter<T3> parameter3, TemplateParameter<T4> parameter4)
            : base(body, parameter1, parameter2, parameter3)
        {
            Parameter4 = parameter4;
        }

        public override string ToString()
        {
            return base.ToString()
                .Replace("<param4>", Parameter4.ToString());
        }
    }
}
