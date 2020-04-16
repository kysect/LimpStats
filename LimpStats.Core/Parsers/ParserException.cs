using System;

namespace LimpStats.Core.Parsers
{
    //TODO: Derive from custom exception
    public class ParserException : Exception
    {
        public ParserException(string message) : base(message)
        {

        }
    }
}