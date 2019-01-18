using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ResultFailureException : Exception
    {
        public ResultFailureException() : base("Result was a failure, you cannot access a result value")
        {
        }

        public ResultFailureException(string message) : base(message)
        {
        }

        public ResultFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResultFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
