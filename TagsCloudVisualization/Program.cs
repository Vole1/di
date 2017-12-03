using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class Program
	{
		public static void Main()
		{
			GenerateInput();
			var builder = new ContainerBuilder();
			builder.RegisterType<CircularCloudLayouter>().As<ICircularCloudLayouter>();
			builder.RegisterType<DefaultImageConfig>().As<IImageConfig>();
			builder.RegisterType<DefaultPreProcessor>().As<IPreProcessor>();
			builder.RegisterType<Visualizer>().As<IVisualizer>();
			builder.RegisterType<TxtReader>().As<IReader>().SingleInstance();

			var container = builder.Build();
			var reader = container.Resolve<IReader>();
			reader.SetFileName("in.txt");
			var visualizer = container.Resolve<IVisualizer>();
			visualizer.Visualize();

		}

		private static void GenerateInput()
		{
			using (var streamWriter = new StreamWriter("in.txt"))
			{
				var words = new[] {"lolo", "lala", "igoGo", "eeeeeeeeeeelka" };
				foreach (var word in words)
				{
					streamWriter.WriteLineAsync(word);
				}
			}

		}
	}
}
