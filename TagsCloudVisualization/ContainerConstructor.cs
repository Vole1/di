using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using TagsCloudVisualization.Infrastructure;
using Component = Castle.MicroKernel.Registration.Component;

namespace TagsCloudVisualization
{
	class ContainerConstructor
	{
		private WindsorContainer Container { get; }

		public ContainerConstructor()
		{
			Container = new WindsorContainer();
			Container.Register(Component.For<ICompositionRoot>().ImplementedBy<CompositionRoot>());

			Container.Register(Component.For<ILayouter>().ImplementedBy<CircularCloudLayouter>());
			Container.Register(Component.For<IImageConfig>().ImplementedBy<DefaultImageConfig>());
			Container.Register(Component.For<IParser>().ImplementedBy<DefaultParser>());
			Container.Register(Component.For<IFilter>().ImplementedBy<DefaultFilter>());
			Container.Register(Component.For<IPreProcessor>().ImplementedBy<DefaultPreProcessor>());
			Container.Register(Component.For<IVisualizer>().ImplementedBy<Visualizer>());
		}

	    public void Run(string inputFileName)
	    {
		    Container.Register(Component.For<IReader>().ImplementedBy<TxtReader>().DependsOn(Dependency.OnValue("filename", inputFileName)));
			var root = Container.Resolve<ICompositionRoot>();
		    root.Run();
		}
    }
}
