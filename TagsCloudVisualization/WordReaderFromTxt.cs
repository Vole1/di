using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	public class WordReaderFromTxt : IWordReader
	{
		private string FileName { get; }
		private string[] ReadStrings { get; set; }

		public WordReaderFromTxt(string fileName)
		{
			FileName = fileName;
			ReadStrings = null;
		}

		public Result<string[]> ReadWordFile()
		{
			var result = new List<string>();
			if (!File.Exists(FileName))
				return new Result<string[]>("File couldn't been found");
			using (var strReader = new StreamReader(FileName))
			{
				while (!strReader.EndOfStream)
					result.Add(strReader.ReadLine());
			}
			ReadStrings = result.ToArray();
			return new Result<string[]>(null, ReadStrings);
		}

		public string[] GetResult()
		{
			if (ReadStrings == null)
				throw new ArgumentException("Reduested data hasn't been produced yet.");
			return ReadStrings;
		}
	}
}
