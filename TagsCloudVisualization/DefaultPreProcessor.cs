using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NHunspell;
using NUnit.Framework.Internal.Execution;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultPreProcessor : IPreProcessor
	{
		private IReader Reader { get; }
		private IImageConfig ImageConfig { get; }
		private readonly string[] nonGrataWords = {"in", "at", "on", "under", "above", "over", "me", "you", "he", "she", "it",
			"we", "they", "my", "your", "his", "her", "its", "our", "their" };

		public DefaultPreProcessor(IReader reader, IImageConfig imageConfig)
		{
			Reader = reader;
			ImageConfig = imageConfig;
		}

		public Word[] PreProcess()
		{
			var input = Reader.ReadFile();
			var wordsDict = new Dictionary<string, int>();

			foreach (var str in input)
			{
				var lowerCaseStr = str.ToLower();
				if (nonGrataWords.Contains(lowerCaseStr))
					continue;

				if (wordsDict.ContainsKey(lowerCaseStr))
					wordsDict[lowerCaseStr] += 1;
				else
					wordsDict[lowerCaseStr] = 1;
			}
			return wordsDict.OrderBy(pair => pair.Value).Select(pair =>
			{
				var wordFontSize = ImageConfig.MinFontSize +
				                   (ImageConfig.MaxFontSize - ImageConfig.MinFontSize) * pair.Value / wordsDict.Count;
				return new Word(pair.Key, new Font(ImageConfig.WordsFont.FontFamily, wordFontSize), ImageConfig.GetWordBrush(pair.Key));
			}).ToArray();
		}
	}
}
