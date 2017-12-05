using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	public class TxtReader : IReader
	{
		public TxtReader(string fileName)
		{
			FileName = fileName;
		}

		private string FileName { get; set; }

		//public void SetFileName(string fileName)
		//{
		//	FileName = fileName;
		//}

		public string[] ReadFile()
		{
			var result = new List<string>();
			using (var strReader = new StreamReader(FileName))
			{
				while (!strReader.EndOfStream)
					result.Add(strReader.ReadLine());

			}
			return result.ToArray();
		}
	}
}
