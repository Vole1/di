using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;
using Moq;

namespace TagsCloudVisualization
{
	[TestFixture]
	class DefaultPreProcessorTests
	{
		[Test]
		public void ForValidInputWithRestrictedWords_Preprocessor_RetrunsValidOutput()
		{
			var input = new[] { "hi", "heRe", "There", "mE", "in", "other", "TREE", "beautiful", "welL" };
			var wordsTuples = new[]
			{
				new Tuple<string, int>("hi", 1),
				new Tuple<string, int>("here", 1),
				new Tuple<string, int>("there", 1),
				new Tuple<string, int>("other", 1),
				new Tuple<string, int>("tree", 1),
				new Tuple<string, int>("beautiful", 1),
				new Tuple<string, int>("well", 1)
			};

			PerformTest(input, wordsTuples);
		}

		[Test]
		public void ForValidRepeatingLowerCaseInput_Preprocessor_RetrunsValidOutput()
		{
			var input = new[] { "hi", "heRe", "beautiful", "hi", "hi", "in", "other", "TREE", "in", "beautiful", "in", "welL" };
			var wordsTuples = new[]
			{
				new Tuple<string, int>("hi", 3),
				new Tuple<string, int>("beautiful", 2),
				new Tuple<string, int>("here", 1),
				new Tuple<string, int>("other", 1),
				new Tuple<string, int>("tree", 1),
				new Tuple<string, int>("well", 1)
			};

			PerformTest(input, wordsTuples);
		}

		private void PerformTest(string[] input, Tuple<string, int>[] wordsTuples)
		{
			var defaultImageConfig = new DefaultImageConfig();
			
			var output = wordsTuples
				.Select(tuple => GetWordFromString(defaultImageConfig, tuple.Item1, tuple.Item2, wordsTuples.Length)).ToArray();

			var readerMock = new Mock<IReader>();
			readerMock.Setup(rd => rd.ReadFile()).Returns(input);

			var dpp = new DefaultPreProcessor(readerMock.Object, defaultImageConfig);
			var result = dpp.PreProcess();
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
			return new Word(wordValue, new Font(imgConfg.WordsFont.FontFamily, fontSize), imgConfg.GetWordBrush(wordValue));
		}
	}
}