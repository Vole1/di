using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class ContainerConstructor
	{
		private readonly IContainer container;
		public ContainerConstructor()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
			builder.RegisterType<DefaultImageConfig>().As<IImageConfig>();
			builder.RegisterType<DefaultPreProcessor>().As<IPreProcessor>();
			builder.RegisterType<Visualizer>().As<IVisualizer>();
			builder.RegisterType<TxtReader>().As<IReader>().SingleInstance();

			container = builder.Build();
		}

		public void Contruct(string filename)
		{
			container.Resolve<IReader>(new NamedParameter("fileName", filename));
			var reader = container.Resolve<IReader>();
			//reader.SetFileName("in.txt");
		}

		public void SaveImage(string imageName)
		{
			var visualizer = container.Resolve<IVisualizer>();
			visualizer.Visualize(imageName);
		}

		public Bitmap GetImage(string imageName)
		{
			var visualizer = container.Resolve<IVisualizer>();
			return visualizer.GetImage();
		}
	}
}
