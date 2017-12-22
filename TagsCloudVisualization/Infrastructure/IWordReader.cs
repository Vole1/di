using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Infrastructure
{
	public interface IWordReader
	{
		Result<string[]> ReadWordFile();

		string[] GetResult();
	}
}
