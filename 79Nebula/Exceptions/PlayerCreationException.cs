using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Exceptions
{
    public class PlayerCreationException : Exception
    {
        public PlayerCreationException()
        {
        }

        public PlayerCreationException(string message) : base(message)
        {
        }

        public PlayerCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlayerCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
