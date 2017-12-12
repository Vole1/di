using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	[TestFixture]
	class DefaultParserTests
	{
		[Test]
		public void ForValidInputWithRestrictedWords_Preprocessor_RetrunsValidOutput()
		{
			var input = new[] {"hi", "here", "there", "me", "in", "other", "tree", "beautiful", "well"};
			var wordsTuples = new[]
			{
				new Tuple<string, int>("hi", 1),
				new Tuple<string, int>("here", 1),
				new Tuple<string, int>("there", 1),
				new Tuple<string, int>("me", 1),
				new Tuple<string, int>("in", 1),
				new Tuple<string, int>("other", 1),
				new Tuple<string, int>("tree", 1),
				new Tuple<string, int>("beautiful", 1),
				new Tuple<string, int>("well", 1)
			};

			PerformTest(input, wordsTuples);
		}


		private void PerformTest(string[] input, Tuple<string, int>[] wordsTuples)
		{
			var defaultImageConfig = new DefaultImageConfig();

			var output = wordsTuples
				.Select(tuple => GetWordFromString(defaultImageConfig, tuple.Item1, tuple.Item2, wordsTuples.Length)).ToArray();

			var dpp = new DefaultParser(defaultImageConfig);
			var result = dpp.ParseWords(input);
			result.ShouldAllBeEquivalentTo(output, options => options.Including(obj =>
				obj.SelectedMemberInfo.MemberType == typeof(Brush) ||
				obj.SelectedMemberInfo.MemberType == typeof(string) ||
				obj.SelectedMemberInfo.MemberType == typeof(Font) ||
				obj.SelectedMemberInfo.MemberType == typeof(float) ||
				obj.SelectedMemberInfo.MemberType == typeof(Color)));
		}

		private Word GetWordFromString(IImageConfig imgConfg, string wordValue, int wordCount, int wordsCount)
		{
			float fontSize = imgConfg.MinFontSize + (imgConfg.MaxFontSize - imgConfg.MinFontSize) * wordCount / wordsCount;
			return new Word(wordValue, new Font(imgConfg.WordsFont.FontFamily, fontSize), imgConfg.GetWordBrush(wordValue), new Size(1,1));
		}
	}
}