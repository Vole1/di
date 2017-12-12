using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	public class DefaultFilter : IFilter
	{
		private string[] NonGrataWords { get; }

		public DefaultFilter()
		{
			NonGrataWords = new[]
			{
				"in", "at", "on", "under", "above", "over", "me", "you", "he", "she", "it",
				"we", "they", "my", "your", "his", "her", "its", "our", "their"
			};
		}

		public DefaultFilter(string[] nonGrataWords)
		{
			NonGrataWords = nonGrataWords;
		}

		public bool Validate(string word)
		{
			return !NonGrataWords.Contains(word);
		}
	}
}