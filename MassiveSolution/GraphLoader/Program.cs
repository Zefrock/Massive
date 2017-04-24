using System;
using System.Configuration;
using System.Threading.Tasks;

namespace GraphLoader
{
    class Program
    {
        const string GraphFolderKeyName = "GraphFolder";

        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(UnobservedTaskExceptionHandler);

        }


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

            var folderLoader = new FolderLoader(graphFolder);
            var files = folderLoader.GetNodeFiles();

            Graph graph = new Graph();
            graph.Load(files);
            graph.Persist();

            Console.WriteLine("The new Graph has been persisted to the DB");
        }


        private static void UnobservedTaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            Console.WriteLine("An unhandled exception occurred");

            switch(ex)
            {
                case AggregateException aex:
                    Console.WriteLine(aex.Flatten().ToString());
                    break;
                default:
                    Console.WriteLine(ex.ToString());
                    break;
                case null:
                    break;
            }
        }
    }
}
