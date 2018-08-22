using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IocLabo.Activators;
using IocLabo.IOC;
using Moq;
using System.Collections.Generic;
using System.Text;

namespace IocLabo.Tests.Activators
{
    [TestClass]
    public class LaboActivatorTests
    {
        Mock<IIoCContainer> ioc;
        ILaboActivator activator;

        [TestInitialize]
        public void Initialize()
        {
            ioc = new Mock<IIoCContainer>();
            activator = new LaboActivator(ioc.Object);
        }

        [TestMethod]
        public void Construct_Simple_NoParams_ParamNumIsZero()
        {
            // Act
            var result = activator.Construct<Simple>();
            // Assert
            Assert.AreEqual(0, result.ParamNum);
        }

        [TestMethod]
        public void Construct_Simple_1IntParam_ParamNumIs1TypeIsInt()
        {
            // Act
            var result = activator.Construct<Simple>(typeof(int));
            // Assert
            Assert.AreEqual(1, result.ParamNum);
            Assert.AreEqual(typeof(int), result.Type);
        }

        [TestMethod]
        public void COnstruct_Simple_1StringParam_ParamNumIs1TypeIsString()
        {
            // Act
            var result = activator.Construct<Simple>(typeof(string));
            // Assert
            Assert.AreEqual(1, result.ParamNum);
            Assert.AreEqual(typeof(string), result.Type);
        }

        [TestMethod]
        public void COnstruct_Simple_3Params_ParamNumIs3()
        {
            // Act
            var result = activator.Construct<Simple>(typeof(int), typeof(int), typeof(string));
            // Assert
            Assert.AreEqual(3, result.ParamNum);
        }

        [TestMethod]
        public void Construct_Simple_2ParamsWithDefaultValue_ParametersAreDefaultValue()
        {
            // Act
            var result = activator.Construct<Simple>(typeof(int), typeof(int));
            // Assert
            Assert.AreEqual(0, result.DefaultValue1);
            Assert.AreEqual(4, result.DefaultValue2);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivatorException))]
        public void Construct_Simple_1StringBuilderParam_ThrowException()
        {
            // Act
            var result = activator.Construct<Simple>(typeof(StringBuilder));
        }

        [TestMethod]
        public void ConstructByLongestArgs_Simple_ParamNumIs3()
        {
            // Act
            var result = activator.ConstructByLongestArgs<Simple>();
            // Assert
            Assert.AreEqual(3, result.ParamNum);
        }

        [TestMethod]
        public void Construct_HasINterfaceConstructor_IBaseIsRegiteredIOC_ParamTypeIsBase()
        {
            // Arrange
            ioc.Setup(i => i.GetImplementType(typeof(IBase))).Returns(typeof(Base));
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBase))).Returns(true);
            // Act
            var result = activator.Construct<HasInterfaceConstructor>(typeof(IBase));
            // Assert
            Assert.AreEqual(typeof(Base), result.ParamType);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivatorException))]
        public void Construct_HasInterfaceCOnstructor_IsNotRegistered_ThrowException()
        {
            // Arrange
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(false);
            // Act
            var result = activator.Construct<HasInterfaceConstructor>(typeof(IBase));
        }

        [TestMethod]
        public void Construct_HasInterfaceConsturctor_IBaseIsRegisteredSingleton_GetSingletonObjectInParam()
        {
            // Arrange
            var single = new Base();
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetSingleton(typeof(IBase))).Returns(single);
            // Act
            var result = activator.Construct<HasInterfaceConstructor>(typeof(IBase));
            // Assert
            Assert.AreEqual(single, result.ParamObject);
        }

        [TestMethod]
        public void ConstructByLongestArgs_HasINterfaceConstructor_IBaseIsRegiteredIOC_ParamTypeIsBase()
        {
            // Arrange
            ioc.Setup(i => i.GetImplementType(typeof(IBase))).Returns(typeof(Base));
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBase))).Returns(true);
            // Act
            var result = activator.ConstructByLongestArgs<HasInterfaceConstructor>();
            // Assert
            Assert.AreEqual(typeof(Base), result.ParamType);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivatorException))]
        public void ConstructByLongestArgs_HasInterfaceCOnstructor_IsNotRegistered_ThrowException()
        {
            // Arrange
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(false);
            // Act
            var result = activator.ConstructByLongestArgs<HasInterfaceConstructor>();
        }

        [TestMethod]
        public void ConstructByLongestArgs_HasInterfaceConsturctor_IBaseIsRegisteredSingleton_GetSingletonObjectInParam()
        {
            // Arrange
            var single = new Base();
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetSingleton(typeof(IBase))).Returns(single);
            // Act
            var result = activator.ConstructByLongestArgs<HasInterfaceConstructor>();
            // Assert
            Assert.AreEqual(single, result.ParamObject);
        }


        [TestMethod]
        public void Construct_ConfiusedClass_1Param_BasicIsTYpeOfDIffBasic()
        {
            // Arrange
            ioc.Setup(i => i.GetImplementType(typeof(IBase))).Returns(typeof(Base));
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetImplementType(typeof(IBasic))).Returns(typeof(DiffBasic));
            ioc.Setup(i => i.IsRegistered(typeof(IBasic))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBasic))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBasic))).Returns(true);
            // Act
            var result = activator.Construct<ConfusedClass>(typeof(IBasic));
            // Assert
            Assert.AreEqual(typeof(DiffBasic), result.basic.GetType());
        }

        [TestMethod]
        public void COnstructLongestArgs_ConfiusedClass()
        {
            // Arrange
            ioc.Setup(i => i.GetImplementType(typeof(IBase))).Returns(typeof(Base));
            ioc.Setup(i => i.IsRegistered(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBase))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBase))).Returns(true);
            ioc.Setup(i => i.GetImplementType(typeof(IBasic))).Returns(typeof(DiffBasic));
            ioc.Setup(i => i.IsRegistered(typeof(IBasic))).Returns(true);
            ioc.Setup(i => i.IsRegisteredSingleton(typeof(IBasic))).Returns(false);
            ioc.Setup(i => i.IsRegisteredImplement(typeof(IBasic))).Returns(true);
            // Act
            var result = activator.ConstructByLongestArgs<ConfusedClass>();
        }

        private class Simple
        {
            public int ParamNum { get; }
            public Type Type { get; }
            public int DefaultValue1;
            public int DefaultValue2;
            public Simple() { ParamNum = 0; }
            public Simple(int a) { ParamNum = 1; Type = typeof(int); }
            public Simple(string b) { ParamNum = 1; Type = typeof(string); }
            public Simple(int a, int b = 4)
            {
                DefaultValue1 = a;
                DefaultValue2 = b;
            }
            public Simple(int a, int b, string c) { ParamNum = 3; }
        }
       
        private class HasInterfaceConstructor
        {
            public Type ParamType { get; }
            public IBase ParamObject { get; }
            public HasInterfaceConstructor(IBase baseClass)
            {
                ParamObject = baseClass;
                ParamType = baseClass.GetType();
            }
        }

        private interface IBase
        {
        }
        private class Base : IBase
        {
        }

        private class ConfusedClass
        {
            public IBasic basic;
            public ConfusedClass(IBasic basic) { this.basic = basic; }

            public ConfusedClass(IBase baseClass, IBasic basic)
            {

            }
        }

        private interface IBasic
        {
        }

        private class Basic : IBasic
        {
        }

        private class DiffBasic : IBasic
        {
            IBase baseClass;
            public DiffBasic(IBase baseClass)
            {
                this.baseClass = baseClass;
            }
        }
    }
}
