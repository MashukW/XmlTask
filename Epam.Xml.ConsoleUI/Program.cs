using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Epam.Xml.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlRoute = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestXML.xml");
            string xPath = "//author";
            
            try
            {
                Dictionary<string, int> elements = ReaderXml.GetElementsDictionary(xmlRoute, xPath);
                PrintElements(elements);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void PrintElements(Dictionary<string, int> elements)
        {
            if (elements.Count == 0)
            {
                Console.WriteLine("This dictionary with elements is empty");
            }
            else
            {
                foreach (var element in elements)
                {
                    Console.WriteLine("Value '{0}' is found in the document {1} times", element.Key, element.Value);
                }

                Console.WriteLine("End dictionary with elemens");
            }

            Console.WriteLine(new string('-', 65));
        }
    }
}
