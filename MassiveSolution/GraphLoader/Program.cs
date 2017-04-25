using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GraphLib.Domain;
using GraphLoader.GraphServices;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GraphLoader
{
    class Program
    {
        const string GraphFolderKeyName = "GraphFolder";
        static IWindsorContainer _container ;

        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(UnobservedTaskExceptionHandler);

            //Defining Windsor container
            _container = new WindsorContainer();

            _container
                .AddFacility<WcfFacility>()
                .Register
                (
                    Component
                        .For<IGraphData>()
                        .AsWcfClient(WcfEndpoint.FromConfiguration("*"))
                );
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

            //Load xml files from folder
            var folderLoader = new FolderLoader(graphFolder);
            var nodes = folderLoader.GetNodes().ToList();

            //Get service from container
            IGraphData graphData = _container.Resolve<IGraphData>();

            //Load data onto the service
            graphData.LoadGraph(nodes);

            Console.WriteLine("The new Graph has been sent to the Data service");
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
