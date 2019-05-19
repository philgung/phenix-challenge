using System;
using System.Runtime.Serialization;

namespace Phenix.Challenge.Domain.Exceptions
{
    [Serializable]
    public class ErrorParseTransactionException : Exception
    {
        public ErrorParseTransactionException()
        {
        }

        public ErrorParseTransactionException(string message) : base(message)
        {
        }

        public ErrorParseTransactionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ErrorParseTransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}