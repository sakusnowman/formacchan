using System;
using System.Collections.Generic;
using System.Text;

namespace IocLabo.Activators
{
    public interface ILaboActivator
    {
        /// <summary>
        /// Construct TClass by argTypes parameter constructor.
        /// Resolve from container if you regiseterd interfaceType, when argType is Interface.
        /// Use default value, if you've never registered argType.
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="argTypes">TClass must have constructor which has these argTypes parameter.</param>
        /// <returns></returns>
        TClass Construct<TClass>(params Type[] argTypes);
        object Construct(Type classType, params Type[] argTypes);

        /// <summary>
        /// Construct TClass by longest args constructor.
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <returns></returns>
        TClass ConstructByLongestArgs<TClass>();
        object ConstructByLongestArgs(Type classType);


        TClass GetDefaultValue<TClass>();
        object GetDefaultValue(Type type);
    }
}
