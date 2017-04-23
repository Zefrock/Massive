using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLoader
{
    class Program
    {
        const string GraphFolderKeyName = "GraphFolder";

        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                Console.WriteLine("GraphLoader is not expecting any arguments");
                return;
            }

            var graphFolder = ConfigurationManager.AppSettings[GraphFolderKeyName];

            if(string.IsNullOrWhiteSpace(graphFolder))
            {
                Console.WriteLine($"Could not find a valid configuration value for key '{GraphFolderKeyName}'");
                return;
            }

            var graphLoader = new GraphLoader(graphFolder);

            graphLoader.Load();
        }
    }
}
