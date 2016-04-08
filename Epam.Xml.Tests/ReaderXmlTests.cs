using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Epam.Xml.Tests
{
    [TestFixture]
    public class ReaderXmlTests
    {
        private ReaderXml _readerXml;
        private string _xmlRoute = "TestXML.xml";
        private string _xPath = "//author";
        private Dictionary<string, int> _actualDictionryElement;

        public static IEnumerable TestCaseWithDataIncorrectForCreateObject
        {
            get
            {
                yield return new TestCaseData("TestXML.xml", "");
                yield return new TestCaseData("", "//author");
                yield return new TestCaseData("D:/Install", "//author");
                yield return new TestCaseData("D:/Install/TestXML.txt", "//author");
                yield return new TestCaseData(null, null);
                yield return new TestCaseData(null, "//author");
                yield return new TestCaseData("D:/Install/TestXML.txt", null);
                yield return new TestCaseData("     ", "//author");
                yield return new TestCaseData("      ", "      ");
            }
        }

        [OneTimeSetUp]
        public void ReaderXmlTestsInit()
        {
            if (_readerXml == null)
                _readerXml = new ReaderXml(_xmlRoute, _xPath);

            _actualDictionryElement = new Dictionary<string, int>
            {
                {"Gambardella, Matthew", 1},
                {"Ralls, Kim", 1},
                {"Corets, Eva", 3},
                {"Randall, Cynthia", 1},
                {"Thurman, Paula", 1},
                {"Knorr, Stefan", 1},
                {"Kress, Peter", 1},
                {"O'Brien, Tim", 2},
                {"Galos, Mike", 1}
            };
        }

        [Test, TestCaseSource(nameof(TestCaseWithDataIncorrectForCreateObject))]
        public void CreateObjectTypeOfReaderXml_UsingNullOrEmptyXmlRouteOrReaderXml_ExpectedArgumentNullException(
            string route,
            string xPathExp)
        {
            Assert.That(() => new ReaderXml(route, xPathExp),
                Throws.InstanceOf(typeof (ArgumentException)));
        }

        [Test]
        public void GetElementsDictionary_UsingCorrectXmlRouteAndReaderXml_ExpectedValidDictionaryElements()
        {
            var expectedDictionaryElement = _readerXml.GetElementsDictionary();

            CollectionAssert.AreEquivalent(expectedDictionaryElement, _actualDictionryElement);
        }

        [Test]
        public void GetElementsDictionary_UsingInvalidPathToXml_ExpectedArgumentException()
        {
            string incorrectPathToXml = "D:/Install/TestXML.xml";

            ReaderXml readerXmlWithIncorrectPath = new ReaderXml(incorrectPathToXml, "//author");

            Assert.That(()=> readerXmlWithIncorrectPath.GetElementsDictionary(),
                Throws.InstanceOf(typeof(FileNotFoundException)));
        }
    }
}
