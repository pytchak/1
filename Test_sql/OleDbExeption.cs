using System;
using System.Runtime.Serialization;

namespace AdoNetConsoleApp
{
    [Serializable]
    internal class OleDbExeption : Exception
    {
        public OleDbExeption()
        {
        }

        public OleDbExeption(string message) : base(message)
        {
        }

        public OleDbExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OleDbExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}