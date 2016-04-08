using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;

namespace Epam.Xml
{
    public class ReaderXml
    {
        private string _xmlRoute;

        public ReaderXml(string xmlRoute, string xPath)
        {
            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentException(nameof(xPath));

            CheckPath(xmlRoute);

            _xmlRoute = xmlRoute;
            XPath = xPath;
        }

        public string XPath { get; set; }

        public string XmlRoute
        {
            get
            {
                return _xmlRoute;
            }
            set
            {
                CheckPath(value);
                _xmlRoute = value;
            }
        }
        
        public Dictionary<string, int> GetElementsDictionary()
        {
            XPathDocument document;
            Dictionary<string, int> result = new Dictionary<string, int>();
            
            try
            {
                document = new XPathDocument(_xmlRoute);
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException(e.Message);
            }

            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select(XPath);

            while (nodes.MoveNext())
            {
                AddToDictionary(result, nodes.Current);
            }

            return result;
        }

        private static void AddToDictionary(Dictionary<string, int> dictionary, XPathItem node)
        {
            if (dictionary.ContainsKey(node.Value))
                dictionary[node.Value]++;
            else
                dictionary.Add(node.Value, 1);
        }

        private static void CheckPath(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Path is null or empty");

            if (Path.GetExtension(path) != ".xml")
                throw new ArgumentException("Path doesn't go to a file with XML extension");
        }
    }
}
