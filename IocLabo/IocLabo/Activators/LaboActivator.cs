using System;
using System.Linq;
using IocLabo.IOC;

namespace IocLabo.Activators
{
    public class LaboActivator : ILaboActivator
    {
        private readonly IIoCContainer ioc;

        public LaboActivator(IIoCContainer ioc)
        {
            this.ioc = ioc;
        }

        public TClass Construct<TClass>(params Type[] argTypes) => (TClass)Construct(typeof(TClass), argTypes);

        public object Construct(Type classType, params Type[] argTypes)
        {
            try
            {
                var constructor = classType.GetConstructor(argTypes);
                object[] args = constructor.GetParameters().Select(p =>
                {
                    if (p.HasDefaultValue) return p.DefaultValue;
                    return GetDefaultValue(p.ParameterType);
                }).ToArray();
                return Activator.CreateInstance(classType, args);
            }
            catch (Exception e)
            {
                throw new ActivatorException("Failed to Construct Instance", e);
            }
        }

        public TClass ConstructByLongestArgs<TClass>() => (TClass)ConstructByLongestArgs(typeof(TClass));

        public object ConstructByLongestArgs(Type classType)
        {
            var constructors = classType.GetConstructors();
            if (constructors.Count() == 0) return GetDefaultValue(classType);

            var longestConstructor = constructors.
                OrderByDescending(c => c.GetParameters().Count()).First();
            var parameterTypes = longestConstructor.GetParameters().Select(p => p.ParameterType);
            return Construct(classType, parameterTypes.ToArray());
        }

        public TClass GetDefaultValue<TClass>() => (TClass)GetDefaultValue(typeof(TClass));
        public object GetDefaultValue(Type type)
        {
            if (type.IsInterface) return GetInterfaceRegisteredValue(type);
            if (type.IsValueType) return Activator.CreateInstance(type);

            if (type.Equals(typeof(string))) return "";

            return null;
        }

        private object GetInterfaceRegisteredValue(Type interfaceType)
        {
            if (ioc.IsRegisteredSingleton(interfaceType))
                return ioc.GetSingleton(interfaceType);
            return ConstructByLongestArgs(ioc.GetImplementType(interfaceType));
        }
    }
}
