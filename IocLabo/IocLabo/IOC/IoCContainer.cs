using System;
using System.Collections.Generic;
using System.Text;

namespace IocLabo.IOC
{
    public class IoCContainer : IIoCContainer
    {
        private Dictionary<Type, Type> interfaceTable;
        private Dictionary<Type, object> singletonTable;

        public IoCContainer()
        {
            interfaceTable = new Dictionary<Type, Type>();
            singletonTable = new Dictionary<Type, object>();
        }

        public void Register<TInterface, TImplement>()
        {
            IOCException.CheckImplementedInterface<TInterface, TImplement>();
            if (IsRegisteredImplement<TInterface>())
            {
                interfaceTable[typeof(TInterface)] = typeof(TImplement);
                return;
            }
            interfaceTable.Add(typeof(TInterface), typeof(TImplement));
        }

        public void RegisterSingleton<TInterface>(object value)
        {
            IOCException.CheckImplementedInterface<TInterface>(value.GetType());
            if (IsRegisteredSingleton<TInterface>())
            {
                singletonTable[typeof(TInterface)] = value;
                return;
            }
            singletonTable.Add(typeof(TInterface), value);
        }

        public TInterface GetSingleton<TInterface>() => (TInterface)GetSingleton(typeof(TInterface));

        public object GetSingleton(Type interfaceType)
        {
            if (IsRegisteredSingleton(interfaceType))
            {
                return singletonTable[interfaceType];
            }
            throw IOCException.NotRegisteredException(interfaceType);
        }

        public Type GetImplementType<TInterface>() => GetImplementType(typeof(TInterface));
        public Type GetImplementType(Type interfaceType)
        {
            if (IsRegisteredImplement(interfaceType))
            {
                return interfaceTable[interfaceType];
            }
            throw IOCException.NotRegisteredException(interfaceType);
        }

        public void Reset()
        {
            singletonTable.Clear();
            interfaceTable.Clear();
        }

        public bool IsRegistered<TInterface>() => IsRegistered(typeof(TInterface));
        public bool IsRegistered(Type interfaceType)
        {
            return IsRegisteredSingleton(interfaceType) || IsRegisteredImplement(interfaceType);
        }

        public bool IsRegisteredSingleton<TInterface>() => IsRegisteredSingleton(typeof(TInterface));

        public bool IsRegisteredSingleton(Type interfaceType)
        {
            return singletonTable.ContainsKey(interfaceType);
        }

        public bool IsRegisteredImplement<Tinterface>() => IsRegisteredImplement(typeof(Tinterface));

        public bool IsRegisteredImplement(Type interfaceType)
        {
            return interfaceTable.ContainsKey(interfaceType);
        }
    }
}
