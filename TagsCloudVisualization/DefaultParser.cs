using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultParser : IParser
	{
		private IImageConfig ImageConfig { get; }

		public DefaultParser(IImageConfig imageConfig)
		{
			ImageConfig = imageConfig;
		}

		public IEnumerable<Word> ParseWords(IEnumerable<string> readWords)
		{
			var graphics = Graphics.FromImage(new Bitmap(1,1));
			var wordsDict = new Dictionary<string, int>();

			foreach (var readWord in readWords)
			{
				if (wordsDict.ContainsKey(readWord))
					wordsDict[readWord] += 1;
				else
					wordsDict[readWord] = 1;
			}
			return wordsDict.OrderBy(pair => pair.Value).Select(pair =>
			{
				var word = pair.Key;
				var wordCount = pair.Value;
				var wordFontSize = ImageConfig.MinFontSize +
								   (ImageConfig.MaxFontSize - ImageConfig.MinFontSize) * wordCount / wordsDict.Count;
				var wordFont = new Font(ImageConfig.WordsFont.FontFamily, wordFontSize);

				var wordDrawingSizeF = graphics.MeasureString(word, wordFont);
				var wordDrawingSize = new Size((int)Math.Round(wordDrawingSizeF.Width)+1, (int)Math.Round(wordDrawingSizeF.Height)+1);

				return new Word(word, wordFont, ImageConfig.GetWordBrush(wordDrawingSize), wordDrawingSize);
			});
		}
	}
}
