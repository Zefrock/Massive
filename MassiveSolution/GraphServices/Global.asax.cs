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
        /// <summary>
        /// Castle windsor container together with WCF Facility allows for inline setup of Service Host
        /// These configurations could be in a xml file, not necessary to prove knowledge of dependency injection concept
        /// </summary>
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

                    //Domain object
                    Component
                        .For<ISerializer<GraphDTO>>()
                        .ImplementedBy<JSonSerializer<GraphDTO>>()
                        .Named("JSonSerializer"),

                    //Service Api (management)
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

                    //Service Data (thin client)
                    Component
                        .For<IGraphData>()
                        .ImplementedBy<GraphData>()
                        .AsWcfService(
                            new DefaultServiceModel()
                            .PublishMetadata(x => x.EnableHttpGet())
                            .AddEndpoints(
                                    WcfEndpoint
                                        .ForContract<IGraphData>()
                                        .BoundTo(new BasicHttpBinding())
                                        .At("GraphData"))
                            .Hosted())
                        .IsDefault(),

                    //Service Domain (path algo)
                    Component
                        .For<IGraphDomain>()
                        .ImplementedBy<GraphDomain>()
                        .AsWcfService(
                            new DefaultServiceModel()
                            .PublishMetadata(x => x.EnableHttpGet())
                            .AddEndpoints(
                                    WcfEndpoint
                                        .ForContract<IGraphDomain>()
                                        .BoundTo(new BasicHttpBinding())
                                        .At("GraphDomain"))
                            .Hosted())
                        .IsDefault()
                );
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