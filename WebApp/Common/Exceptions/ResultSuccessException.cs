using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ResultSuccessException : Exception
    {
        public ResultSuccessException() : base("Result was sucessful, you cannot access exception message")
        {
        }

        public ResultSuccessException(string message) : base(message)
        {
        }

        public ResultSuccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResultSuccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
