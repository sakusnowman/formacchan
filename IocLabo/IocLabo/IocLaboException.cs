using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IocLabo
{
    public class IocLaboException : Exception
    {
        public IocLaboException() { }

        public IocLaboException(string message) : base(message) { }

        public IocLaboException(string message, Exception innerException) : base(message, innerException) { }

        protected IocLaboException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
