using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;

namespace Epam.Xml
{
    /// <summary>
    /// ReaderXml is class which describe work with xml file.
    /// </summary>
    public class ReaderXml
    {
        /// <summary>
        /// Create dictionary which contains elements found by xPath.
        /// </summary>
        /// <param name="xmlRoute">contains path to xml file.</param>
        /// <param name="xPath">contains string with xPath.</param>/// 
        /// <returns>dictionary with elements</returns>
        /// <remarks>method doesn't work wiht a big size files.</remarks>
        /// <exception cref="ArgumentException">
        /// xmlRoute or xpath is null, empty, contains only white space
        /// or doesn't have an extension '.xml'
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// file not found
        /// </exception>
        public static Dictionary<string, int> GetElementsDictionary(string xmlRoute, string xPath)
        {
            CheckArguments(xmlRoute, xPath);
            
            XPathDocument document = new XPathDocument(xmlRoute);
            XPathNavigator navigator = document.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select(xPath);
            
            Dictionary<string, int> result = new Dictionary<string, int>();

            while (nodes.MoveNext())
            {
                AddToDictionary(result, nodes.Current);
            }

            return result;
        }
        
        private static void AddToDictionary(Dictionary<string, int> dictionary, XPathItem node)
        {
            if (dictionary.ContainsKey(node.Value))
            {
                dictionary[node.Value]++;
            }
            else
            {
                dictionary.Add(node.Value, 1);
            }
        }
        
        private static void CheckArguments(string route, string xPath)
        {
            if (string.IsNullOrWhiteSpace(xPath))
            {
                throw new ArgumentException(nameof(xPath));
            }

            if (string.IsNullOrWhiteSpace(route))
            {
                throw new ArgumentException("Path should not be null or empty");
            }

            if (Path.GetExtension(route) != ".xml")
            {
                throw new ArgumentException("Path doesn't go to a file with XML extension");
            }
        }
    }
}
