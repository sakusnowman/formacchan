using FormacchanLibrary.Models;
using FormacchanLibrary.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formacchan.Tests.Services
{
    [TestFixture]
    public class FormatKeyValuePairsTests
    {
        IFormatKeyValuePairsService service;
        [SetUp]
        public void Setup()
        {
            service = new FormatKeyValuePairsService();

            sample = new SimpleSample() { Name = "SAMPLE1", Amount = 1 };
            hasSample = new HasSimpleSample() { Name = "HAS1", SS = sample };
            hasHasSample = new HasHasSimpleSample() { Name = "HASHAS1", HSS = hasSample };
        }

        [Test]
        public void GetFormatKeyValuePairs()
        {
            // Arrange
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{Name}<=>SAMPLE1");
            builder.AppendLine("{Amount}<=>1");
            var expectedPair = new FormatKeyValuePair("{Name}", "SAMPLE1");
            var expectedPair1 = new FormatKeyValuePair("{Amount}", "1");
            // Act
            var result = service.GetFormatKeyValuePairs(builder.ToString()).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedPair, result[0]);
            Assert.AreEqual(expectedPair1, result[1]);
        }

        [Test]
        public void GetFormatKeyValuePairs_ValueIsEmpty_ValueIsStringEmpty()
        {
            // Arrange
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{Name}<=>");
            builder.AppendLine("{Amount}<=>1");
            var expectedPair = new FormatKeyValuePair("{Name}", "");
            var expectedPair1 = new FormatKeyValuePair("{Amount}", "1");
            // Act
            var result = service.GetFormatKeyValuePairs(builder.ToString()).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedPair, result[0]);
            Assert.AreEqual(expectedPair1, result[1]);
        }

        [Test]
        public void GetFormatKeyValuePairFromProperties_SimpleSample_GetOnlyPublicMembers()
        {
            //Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "SAMPLE1");
            var expectedResult1 = new FormatKeyValuePair("{Amount}", "1");
            // Act
            var result = service.GetFormatKeyValuePairFromProperties(sample).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedResult, result[0]);
            Assert.AreEqual(expectedResult1, result[1]);
        }

        [Test]
        public void GetFormatKeyValuePairFromProperties_HasSimpleSample_GetWithSimpleSampleProperties()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HAS1");
            var expectedResult1 = new FormatKeyValuePair("{SS::Name}", "SAMPLE1");
            var expectedResult2 = new FormatKeyValuePair("{SS::Amount}", "1");
            // Act
            var result = service.GetFormatKeyValuePairFromProperties(hasSample).ToList();
            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(expectedResult, result[0]);
            Assert.AreEqual(expectedResult1, result[1]);
            Assert.AreEqual(expectedResult2, result[2]);
        }

        [Test]
        public void GetFormatKeyValuePairFromProperties_HasSimpleSample_getPropertiesInNoValueTypePropertyIsFalse__GetWithSimpleSampleToString()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HAS1");
            var expectedResult1 = new FormatKeyValuePair("{SS}", "SAMPLE1:1");
            // Act
            var result = service.GetFormatKeyValuePairFromProperties(hasSample, getChildlenProperties: false).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedResult, result[0]);
            Assert.AreEqual(expectedResult1, result[1]);
        }

        [Test]
        public void GetFormatKeyValuePairFromProperties_HasHasSimpleSample_GetWithHasSimpleSampleProperties()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HASHAS1");
            var expectedResult1 = new FormatKeyValuePair("{HSS::Name}", "HAS1");
            var expectedResult2 = new FormatKeyValuePair("{HSS::SS::Name}", "SAMPLE1");
            var expectedResult3 = new FormatKeyValuePair("{HSS::SS::Amount}", "1");
            // Act
            var result = service.GetFormatKeyValuePairFromProperties(hasHasSample).ToList();
            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(expectedResult, result[0]);
            Assert.AreEqual(expectedResult1, result[1]);
            Assert.AreEqual(expectedResult2, result[2]);
            Assert.AreEqual(expectedResult3, result[3]);
        }

        [Test]
        public void GetFormatKeyValuePairFromProperties_HasHasSimple_NotGetChildeProperties_GetParentProperties()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HASHAS1");
            var expectedResult1 = new FormatKeyValuePair("{HSS}", hasSample.ToString());
            // Act
            var result = service.GetFormatKeyValuePairFromProperties(hasHasSample, getChildlenProperties: false).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(expectedResult, result[0]);
            Assert.AreEqual(expectedResult1, result[1]);
        }

        [Test]
        public void GetXmlFormatKeyValuePairFromProperties_Sample()
        {
            // Act
            var result = service.GetXmlFormatKeyValuePairFromProperties(sample).ToList();
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("{Name}", result[0].Attribute("key").Value);
            Assert.AreEqual("SAMPLE1", result[0].Attribute("value").Value);
            Assert.AreEqual("{Amount}", result[1].Attribute("key").Value);
            Assert.AreEqual("1", result[1].Attribute("value").Value);
        }

        [Test]
        public void GetXmlFormatKeyValuePairFromProperties_HasHasSimpleSample_GetWithHasSimpleSampleProperties()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HASHAS1");
            var expectedResult1 = new FormatKeyValuePair("{HSS::Name}", "HAS1");
            var expectedResult2 = new FormatKeyValuePair("{HSS::SS::Name}", "SAMPLE1");
            var expectedResult3 = new FormatKeyValuePair("{HSS::SS::Amount}", "1");
            // Act
            var result = service.GetXmlFormatKeyValuePairFromProperties(hasHasSample).ToList();
            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(expectedResult.Key, result[0].Attribute("key").Value);
            Assert.AreEqual(expectedResult.Value, result[0].Attribute("value").Value);
            Assert.AreEqual(expectedResult1.Key, result[1].Attribute("key").Value);
            Assert.AreEqual(expectedResult1.Value, result[1].Attribute("value").Value);
            Assert.AreEqual(expectedResult2.Key, result[2].Attribute("key").Value);
            Assert.AreEqual(expectedResult2.Value, result[2].Attribute("value").Value);
            Assert.AreEqual(expectedResult3.Key, result[3].Attribute("key").Value);
            Assert.AreEqual(expectedResult3.Value, result[3].Attribute("value").Value);
        }

        [Test]
        public void GetXmlFormatKeyValuePairFromProperties_HasHasSimple_NotGetChildeProperties_GetParentProperties()
        {
            // Arrange
            var expectedResult = new FormatKeyValuePair("{Name}", "HASHAS1");
            var expectedResult1 = new FormatKeyValuePair("{HSS}", hasSample.ToString());
            // Act
            var result = service.GetXmlFormatKeyValuePairFromProperties(hasHasSample, getChildlenProperties: false).ToList();
            // Assert
            Assert.AreEqual(expectedResult.Key, result[0].Attribute("key").Value);
            Assert.AreEqual(expectedResult.Value, result[0].Attribute("value").Value);
            Assert.AreEqual(expectedResult1.Key, result[1].Attribute("key").Value);
            Assert.AreEqual(expectedResult1.Value, result[1].Attribute("value").Value);
        }

        private SimpleSample sample;
        private HasSimpleSample hasSample;
        private HasHasSimpleSample hasHasSample;
        class SimpleSample
        {
            public string Name { get; set; }
            public int Amount { get; set; }
            private string Dummy { get; set; }
            public override string ToString()
            {
                return Name + ":" + Amount;
            }
        }

        class HasSimpleSample
        {
            public string Name { get; set; }
            public SimpleSample SS { get; set; }
            public override string ToString()
            {
                return Name + ":" + SS.ToString();
            }
        }

        class HasHasSimpleSample
        {
            public string Name { get; set; }
            public HasSimpleSample HSS { get; set; }
        }

    }
}