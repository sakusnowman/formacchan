using System;
using System.Collections.Generic;
using System.Text;
using IocLabo.IOC;
using IocLabo.Activators;

namespace IocLabo
{
    /// <summary>
    /// This class support IoC pattern.
    /// You can easyily to use IoC Container.
    /// This methods are not supported null object;
    /// </summary>
    /// <remarks>
    /// This class can Reset, Register, RegisterSingleton, Resolve, Construct and ConstructLongestArgs
    /// </remarks>
    public class Labo
    {
        static private IIoCContainer ioc = new IoCContainer();
        static private ILaboActivator activator = new LaboActivator(ioc);

        public static void SetIocAndActivator(IIoCContainer ioC, ILaboActivator laboActivator)
        {
            ioc = ioC;
            activator = laboActivator;
        }

        /// <summary>
        /// Reset registerd classes.
        /// </summary>
        public static void Reset() => ioc.Reset();

        /// <summary>
        /// Register interface use which implement class.
        /// </summary>
        /// <typeparam name="TInterface">This must be interface class.</typeparam>
        /// <typeparam name="TImplement">Tihs must be implemented TInterface.</typeparam>
        public static void Register<TInterface, TImplement>() => ioc.Register<TInterface, TImplement>();

        /// <summary>
        /// Register singletonObject to TInterface.
        /// </summary>
        /// <typeparam name="TInterface">This must be interface class.</typeparam>
        /// <param name="value">This must be object which is implemented TInterface</param>
        public static void RegisterSingleton<TInterface>(object value) => ioc.RegisterSingleton<TInterface>(value);

        /// <summary>
        /// Resolve object which is implemented TInterface.
        /// Construct object by longest parameters constractor, if you register only implementType.
        /// </summary>
        /// <typeparam name="TInterface">This is must be registered before you use.</typeparam>
        /// <returns> Singleton object is resolved, if you registered both implementType and singleton.</returns>
        public static object Resolve(Type interfaceType)
        {
            if (ioc.IsRegistered(interfaceType) == false) throw IOCException.NotRegisteredException(interfaceType);
            try
            {
                return ioc.GetSingleton(interfaceType);
            }
            catch (IOCException e)
            {
                return activator.ConstructByLongestArgs(ioc.GetImplementType(interfaceType));
            }
        }
        ///<see cref="Resolve(Type)"/>
        public static TInterface Resolve<TInterface>() => (TInterface)Resolve(typeof(TInterface));

        /// <summary>
        /// Construct TClass by argTypes parameter constructor.
        /// Resolve from container if you regiseterd interfaceType, when argType is Interface.
        /// Use default value, if you've never registered argType.
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="argTypes">TClass must have constructor which has these argTypes parameter.</param>
        /// <returns></returns>
        public static TClass Construct<TClass>(params Type[] argTypes) => activator.Construct<TClass>(argTypes);
        ///<see cref="Construct{TClass}(Type[])"/>
        public static object Construct(Type classType, params Type[] argTypes) => activator.Construct(classType, argTypes);

        /// <summary>
        /// Construct TClass by longest args constructor.
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <returns></returns>
        public static TClass ConstructByLongestArgs<TClass>() => activator.ConstructByLongestArgs<TClass>();
       
        /// <summary>
        /// <see cref="ConstructByLongestArgs{TClass}"/>
        /// </summary>
        public static object ConstructByLongestArgs(Type classType) => activator.ConstructByLongestArgs(classType);
    }
}
