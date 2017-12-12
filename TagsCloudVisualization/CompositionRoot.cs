using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	public class CompositionRoot : ICompositionRoot
	{
		private IReader Reader { get; }
		private IFilter Filter { get; }
		private IPreProcessor PreProcessor { get; }
		private IParser Parser { get; }
		public ILayouter Layouter { get; }
		private IVisualizer Visualizer { get; }

		public CompositionRoot(IReader reader, IFilter filter, IPreProcessor preProcessor, IParser parser, ILayouter layouter, IVisualizer visualizer)
		{
			Reader = reader;
			Filter = filter;
			PreProcessor = preProcessor;
			Parser = parser;
			Layouter = layouter;
			Visualizer = visualizer;
		}

		public void Run()
		{
			var words = Parser.ParseWords(Reader.ReadFile().Select(s => PreProcessor.PreProcess(s)).Where(x => Filter.Validate(x))).ToList();
			var rectangles = Layouter.PutRectangles(words.Select(w => w.WordDrawingSize));
			Visualizer.Visualize(words, rectangles);
		}

	}

	interface ICompositionRoot
	{
		void Run();
	}
}
