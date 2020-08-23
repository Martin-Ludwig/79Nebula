using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nebula._79Nebula.Exceptions
{
    public class EffectNotStackableException : Exception
    {
        public EffectNotStackableException()
        {
        }

        public EffectNotStackableException(string message) : base(message)
        {
        }

        public EffectNotStackableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EffectNotStackableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
