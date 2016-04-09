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
        private string _xmlRoute;
        private string _xPath;
        private Dictionary<string, int> _actualDictionryForAuthorElement;
        private Dictionary<string, int> _actualDictionryForGenreElement;

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
            _xmlRoute = "TestXML.xml";
            _xPath = "//author";

            _actualDictionryForAuthorElement = new Dictionary<string, int>
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

            _actualDictionryForGenreElement = new Dictionary<string, int>
            {
                {"Computer", 4 },
                {"Fantasy", 4 },
                {"Romance", 2 },
                {"Horror", 1 },
                {"Science Fiction", 1 },
            };
        }

        [Test, TestCaseSource(nameof(TestCaseWithDataIncorrectForCreateObject))]
        public void GetElementsDictionary_UsingNullOrEmptyXmlRouteOrReaderXml_ExpectedArgumentException(
            string route,
            string xPathExp)
        {
            Assert.That(() => ReaderXml.GetElementsDictionary(route, xPathExp),
                Throws.InstanceOf(typeof (ArgumentException)));
        }

        [Test]
        public void GetElementsDictionary_UsingCorrectXmlRouteAndXPath_ExpectedDictionryForAuthorElement()
        {
            var expectedDictionaryElement = ReaderXml.GetElementsDictionary(_xmlRoute, _xPath);

            CollectionAssert.AreEquivalent(expectedDictionaryElement, _actualDictionryForAuthorElement);
        }

        [Test]
        public void GetElementsDictionary_UsingChangeXPathForFindGenreValueInformation_ExpectedDictionryForGenreElement()
        {
            var expectedDictionaryElement = ReaderXml.GetElementsDictionary(_xmlRoute, "//genre");

            CollectionAssert.AreEquivalent(expectedDictionaryElement, _actualDictionryForGenreElement);
        }

        [Test]
        public void GetElementsDictionary_UsingInvalidPathToXml_ExpectedFileNotFoundException()
        {
            string incorrectPathToXml = "D:/Install/TestXML.xml";
            
            Assert.That(()=> ReaderXml.GetElementsDictionary(incorrectPathToXml, "//author"),
                Throws.InstanceOf(typeof(FileNotFoundException)));
        }
    }
}
