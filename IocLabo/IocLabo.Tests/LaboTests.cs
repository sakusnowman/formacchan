using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IocLabo;
using IocLabo.IOC;
using IocLabo.Activators;
using Moq;

namespace IocLabo.Tests
{
    /// <summary>
    /// LaboTests の概要の説明
    /// </summary>
    [TestClass]
    public class LaboTests
    {
        Mock<IIoCContainer> ioc;
        Mock<ILaboActivator> activator;

        [TestInitialize]
        public void Initialize()
        {
            ioc = new Mock<IIoCContainer>();
            activator = new Mock<ILaboActivator>();

            Labo.Reset();
            Labo.SetIocAndActivator(ioc.Object, activator.Object);
        }

        [TestMethod]
        public void Register_IBaseWithBase_Success()
        {
            // Act
            Labo.Register<IBase, Base>();
            // Assert
            ioc.Verify(i => i.Register<IBase, Base>());
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void Register_NotInterfaceClass_ThrowExeption()
        {
            // Arrange
            ioc.Setup(i => i.Register<Base, SubBase>()).Throws<IOCException>();
            // Assert
            Labo.Register<Base, SubBase>();
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void Register_NotImplementClass_ThrowExeption()
        {
            // Arrange
            ioc.Setup(i => i.Register<IBase, Sub>()).Throws<IOCException>();
            // Act
            Labo.Register<IBase, Sub>();
        }

        [TestMethod]
        public void RegisterSingleton_IBaseWithBase_Success()
        {
            // Arrange
            var value = new Base();
            // Act
            Labo.RegisterSingleton<IBase>(value);
            // Assert
            ioc.Verify(i => i.RegisterSingleton<IBase>(value));
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void RegisterSingleton_NotInterfaceClass_ThrowExeption()
        {
            // Arrange
            ioc.Setup(i => i.RegisterSingleton<Base>(It.IsAny<SubBase>())).Throws<IOCException>();
            // Act
            Labo.RegisterSingleton<Base>(new SubBase());
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void RegisterSingleton_NotImplementClass_ThrowExeption()
        {
            // Arrange
            ioc.Setup(i => i.RegisterSingleton<IBase>(It.IsAny<Sub>())).Throws<IOCException>();
            // Act
            Labo.RegisterSingleton<IBase>(new Sub());
        }

        [TestMethod]
        public void Resolve_IBaseWhenRegisteredIBaseWithBase_ResolveBase()
        {
            // Arrange
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetSingleton(typeof(IBase))).Throws<IOCException>();
            ioc.Setup(i => i.GetImplementType(typeof(IBase))).Returns(typeof(Base));
            activator.Setup(a => a.ConstructByLongestArgs(typeof(Base))).Returns(new Base());
            // Act
            var result = Labo.Resolve<IBase>();
            // Assert
            Assert.AreEqual(typeof(Base), result.GetType());
        }

        [TestMethod]
        public void Resolve_IBaseWhenRegisteredSingleton_ResolveSingletonBase()
        {
            // Arrange
            var single = new Base();
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetSingleton(typeof(IBase))).Returns(single);
            // Act
            var result = Labo.Resolve<IBase>();
            // Assert
            Assert.AreEqual(single, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IOCException))]
        public void Resolve_IBaseWhenIsNotRegistered_ThrowException()
        {
            // Arrange
            ioc.Setup(i => i.IsRegistered<IBase>()).Returns(false);
            // Act
            var result = Labo.Resolve<IBase>();
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
