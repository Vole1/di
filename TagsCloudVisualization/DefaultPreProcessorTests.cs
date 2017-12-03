using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	[TestFixture]
	class DefaultPreProcessorTests
	{
		[Test]
		public void RetrunsValidOutputForValidInput()
		{
			var input = new string[] {"hi", "heRe", "There", "mE","in", "other", "TREE", "beautiful", "welL" };
			var output = new string[] {"hi", "here", "there", "tree", "beautiful", "well" };

			var testReader = new TestReader {Input = input};
			var dpp = new DefaultPreProcessor(testReader);
			dpp.PreProcess().ShouldBeEquivalentTo(output);
		}

		class TestReader : IReader
		{
			public string[] Input { get; set; }

			public void SetFileName(string fileName)
			{
			}

			public string[] ReadFile()
			{
				return Input;
			}
		}
	}
}
