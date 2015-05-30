using System.IO;

namespace Igloo.Tools
{
    public static class Extensions
    {
        public static void Write(this StreamWriter writer, int indentation, string value)
        {
            writer.Write(new string(' ', indentation));
            writer.Write(value);
        }

        public static void Write(this StreamWriter writer, int indentation, string format, params object[] args)
        {
            writer.Write(new string(' ', indentation));
            writer.Write(format, args);
        }

        public static void WriteLine(this StreamWriter writer, int indentation, string value)
        {
            writer.Write(new string(' ', indentation));
            writer.WriteLine(value);
        }

        public static void WriteLine(this StreamWriter writer, int indentation, string format, params object[] args)
        {
            writer.Write(new string(' ', indentation));
            writer.WriteLine(format, args);
        }
    }
}
