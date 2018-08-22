using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IocLabo.IOC;

namespace IocLabo.Tests.IOC
{
    /// <summary>
    /// IoCContainerTests の概要の説明
    /// </summary>
    [TestClass]
    public class IoCContainerTests
    {
        IIoCContainer ioc;

        [TestInitialize]
        public void Inititlize()
        {
            ioc = new IoCContainer();
        }

        [TestMethod]
        public void Register_IBaseWithBase_Success()
        {
            ioc.Register<IBase, Base>();
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void Register_NotInterfaceClass_ThrowExeption()
        {
            ioc.Register<Base, SubBase>();
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void Register_NotImplementClass_ThrowExeption()
        {
            ioc.Register<IBase, Sub>();
        }

        [TestMethod]
        public void RegisterSingleton_IBaseWithBase_Success()
        {
            ioc.RegisterSingleton<IBase>(new Base());
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void RegisterSingleton_NotInterfaceClass_ThrowExeption()
        {
            ioc.RegisterSingleton<Base>(new SubBase());
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void RegisterSingleton_NotImplementClass_ThrowExeption()
        {
            ioc.RegisterSingleton<IBase>(new Sub());
        }

        [TestMethod]
        public void GetSingleton_IBaseWhenRegisteredIBaseWithBase_ReturnsSameBaseObject()
        {
            // Arrange
            var original = new Base();
            ioc.RegisterSingleton<IBase>(original);
            // Act
            var obj1 = ioc.GetSingleton<IBase>();
            var obj2 = ioc.GetSingleton<IBase>();
            // Assert 
            Assert.AreEqual(original, obj1);
            Assert.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void GetSingleton_NotRegisteredIBase_ThrowException()
        {
            // Act
            var obj1 = ioc.GetSingleton<IBase>();
        }

        [TestMethod]
        public void GetImplementType_IBaseWhenRegisteredIBaseWithBase_ReturnTypeofBase()
        {
            // Arrange
            ioc.Register<IBase, Base>();
            // Act
            var type = ioc.GetImplementType<IBase>();
            // Assert
            Assert.AreEqual(typeof(Base), type);
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void GetSIngleton_NotRegisteredIBase_ThrowException()
        {
            // Act
            var type = ioc.GetImplementType<IBase>();
        }


        private interface IBase
        {
            void Method();
        }

        private class Base : IBase
        {
            public void Method()
            {
            }
        }
        private class SubBase : Base { }
        private class Sub { }
    }
}
