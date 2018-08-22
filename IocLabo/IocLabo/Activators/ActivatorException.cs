using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IocLabo.Activators
{
    public class ActivatorException : IocLaboException
    {
        public ActivatorException() { }

        public ActivatorException(string message) : base(message) { }

        public ActivatorException(string message, Exception innerException) : base(message, innerException) { }

        protected ActivatorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
