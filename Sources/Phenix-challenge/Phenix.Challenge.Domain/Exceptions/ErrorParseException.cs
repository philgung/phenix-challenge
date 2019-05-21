using System;
using System.Runtime.Serialization;

namespace Phenix.Challenge.Domain.Exceptions
{
    [Serializable]
    public class ErrorParseException : Exception
    {
        public ErrorParseException()
        {
        }

        public ErrorParseException(string message) : base(message)
        {
        }

        public ErrorParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ErrorParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}