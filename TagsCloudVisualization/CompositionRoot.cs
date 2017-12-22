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
		private IConfigReader ConfigReader { get; }
		private IWordReader WordReader { get; }
		private IFilter Filter { get; }
		private IPreProcessor PreProcessor { get; }
		private IParser Parser { get; }
		public ILayouter Layouter { get; }
		private IVisualizer Visualizer { get; }

		public CompositionRoot(IConfigReader configReader, IWordReader wordReader, IFilter filter, IPreProcessor preProcessor, IParser parser, ILayouter layouter, IVisualizer visualizer)
		{
			WordReader = wordReader;
			Filter = filter;
			PreProcessor = preProcessor;
			Parser = parser;
			Layouter = layouter;
			Visualizer = visualizer;
			ConfigReader = configReader;
		}

		public Result<bool> Run()
		{
			var stringsParseResult = WordReader.ReadWordFile();
			if (!stringsParseResult.IsSuccess)
				return new Result<bool>(stringsParseResult.Error);

			var words = Parser.ParseWords(WordReader.GetResult().Select(s => PreProcessor.PreProcess(s)).Where(x => Filter.Validate(x))).ToList();
			var rectanglesResult = Layouter.PutRectangles(words.Select(w => w.WordDrawingSize));
			if (!rectanglesResult.IsSuccess)
				return new Result<bool>(rectanglesResult.Error);
			Visualizer.Visualize(words, rectanglesResult.GetValue(), "img1");
			return new Result<bool>(null);
		}
	}

	interface ICompositionRoot
	{
		Result<bool> Run();
	}
}
