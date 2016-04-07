using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace Epam.Xml
{
    public class ReaderXml
    {
        private string _xmlRoute;
        private string _xPath;
        
        public ReaderXml(string xmlRoute, string xPath)
        {
            if (string.IsNullOrWhiteSpace(xmlRoute))
                throw new ArgumentNullException(nameof(xmlRoute));

            if (string.IsNullOrWhiteSpace(xPath))
                throw new ArgumentNullException(nameof(xPath));

            _xmlRoute = xmlRoute;
            _xPath = xPath;
        }
        
        public Dictionary<string, int> GetElementsDictionary()
        {
            XPathDocument document;
            Dictionary<string, int> result = new Dictionary<string, int>();
            
            try
            {
                document = new XPathDocument(_xmlRoute);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new ArgumentException(e.Message + " или указан неправильный путь!");
            }

            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select(_xPath);

            while (nodes.MoveNext())
            {
                AddToDictionary(result, nodes.Current);
            }

            return result;
        }

        private void AddToDictionary(Dictionary<string, int> dictionary, XPathItem node)
        {
            if (dictionary.ContainsKey(node.Value))
                dictionary[node.Value]++;
            else
                dictionary.Add(node.Value, 1);
        }
    }
}
