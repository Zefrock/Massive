using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using GraphLib.Domain;
using GraphLib.Domain.Serializers;
using GraphLib.DTOs;
using System;
using System.ServiceModel;

namespace GraphServices
{
    public class Global : System.Web.HttpApplication
    {
        IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer();

            _container
                .AddFacility<WcfFacility>()
                .Register
                (
                    // To allow declarations of other Setializers
                    Component
                        .For<IGraph>()
                        .ImplementedBy<UnweightedGraph>()
                        .DependsOn(ServiceOverride.ForKey<ISerializer<GraphDTO>>().Eq("JSonSerializer")),

                    Component
                        .For<ISerializer<GraphDTO>>()
                        .ImplementedBy<JSonSerializer<GraphDTO>>()
                        .Named("JSonSerializer"),

                    Component
                        .For<IGraphApi>()
                        .ImplementedBy<GraphApi>()
                        .AsWcfService(
                            new DefaultServiceModel()
                            .PublishMetadata(x => x.EnableHttpGet())
                            .AddEndpoints(
                                    WcfEndpoint
                                        .ForContract<IGraphApi>()
                                        .BoundTo(new BasicHttpBinding())
                                        .At("GraphApi"))
                            .Hosted())
                        .IsDefault(),

                    Component
                        .For<IGraphDataServices>()
                        .ImplementedBy<GraphDataServices>()
                        .AsWcfService(
                            new DefaultServiceModel()
                            .PublishMetadata(x => x.EnableHttpGet())
                            .AddEndpoints(
                                    WcfEndpoint
                                        .ForContract<IGraphDataServices>()
                                        .BoundTo(new BasicHttpBinding())
                                        .At("GraphDataServices"))
                            .Hosted())
                        .IsDefault()
                );
            /*
             * .AddEndPoints(WcfEndpoint.ForContract<ISimpleService>().BoundTo(new BasicHttpBinding()).At("SimpleService"))));*/
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}