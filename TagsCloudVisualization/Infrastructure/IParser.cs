using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface IParser
	{
		IEnumerable<Word> ParseWords(IEnumerable<string> readWord);
	}
}
