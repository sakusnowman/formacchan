using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IocLabo.IOC
{
    public class IOCException : IocLaboException
    {
        public IOCException(){}
        public IOCException(string message) : base(message) { }
        public IOCException(string message, Exception innerException) : base(message, innerException){}
        protected IOCException(SerializationInfo info, StreamingContext context) : base(info, context){}

        public static IOCException NotRegisteredException(Type type)
        {
            StringBuilder message = new StringBuilder();
            message.Append(type.ToString());
            message.Append(" is not registered.");
            return new IOCException(message.ToString());
        }

        public static IOCException NotInterfaceException(Type notInterfaceType)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(notInterfaceType.ToString() + " is not interface.");
            return new IOCException(message.ToString());
        }

        public static void CheckImplementedInterface(Type interfaceType, Type implementedType)
        {
            if (interfaceType.IsInterface == false) throw NotInterfaceException(interfaceType);
            try
            {
                implementedType.GetInterfaceMap(interfaceType);
            }
            catch (Exception e)
            {
                StringBuilder message = new StringBuilder();
                message.Append(implementedType.ToString());
                message.Append(" is not implemented " + interfaceType.ToString() + ".");
                throw new IOCException(message.ToString(), e);
            }
        }

        public static void CheckImplementedInterface<TInterface>(Type type)
            => CheckImplementedInterface(typeof(TInterface), type);

        public static void CheckImplementedInterface<TInterface, TImplement>() 
            => CheckImplementedInterface<TInterface>(typeof(TImplement));
    }
}
