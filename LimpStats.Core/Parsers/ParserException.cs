using System;

namespace LimpStats.Core.Parsers
{
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {
        }
    }
}