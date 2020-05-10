using System;
using System.Collections.Generic;

namespace LightLog
{
    public class LogLevel : IComparable
    {
        public static LogLevel Trace { get; } = new LogLevel(0, "Trace");
        public static LogLevel Debug { get; } = new LogLevel(1, "Debug");
        public static LogLevel Notice { get; } = new LogLevel(2, "Notice");
        public static LogLevel Info { get; } = new LogLevel(3, "Info");
        public static LogLevel Warning { get; } = new LogLevel(4, "Warning");
        public static LogLevel Error { get; } = new LogLevel(5, "Error");
        public static LogLevel Fatal { get; } = new LogLevel(6, "Fatal");

        public string Name { get; }
        public int Value { get; }

        private LogLevel(int val, string name)
        {
            Value = val;
            Name = name;
        }

        public static IEnumerable<LogLevel> ListAll()
        {
            return new[] { Trace, Debug, Notice, Info, Warning, Error, Fatal };
        }

        public override bool Equals(object obj)
        {
            if (!(obj is LogLevel otherValue))
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        int IComparable.CompareTo(object obj)
        {
            return Value.CompareTo(((LogLevel)obj).Value);
        }
    }
}
