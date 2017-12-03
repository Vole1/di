using System;
using System.Collections;
using System.Collections.Generic;
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

		public DefaultPreProcessor(IReader reader)
		{
			Reader = reader;
		}

		public string[] PreProcess()
		{
			var input = Reader.ReadFile();
			var wordsDict = new Dictionary<string, int>();
			var fullPath = Directory.GetParent(System.Reflection.Assembly.GetAssembly(GetType()).Location);

			using (Hunspell hunspell = new Hunspell(File.ReadAllBytes(fullPath + "\\en_us.aff"), File.ReadAllBytes(fullPath + "\\en_us.dic")))
			{
				foreach (var str in input)
				{
					var lowerCaseStr = str.ToLower();
					//var a = hunspell.Analyze("hi");

					if (wordsDict.ContainsKey(lowerCaseStr))
						wordsDict[lowerCaseStr] += 1;
					else
						wordsDict[lowerCaseStr] = 1;
				}
			}
			return wordsDict.OrderBy(pair => pair.Value).Select(pair => pair.Key).ToArray();
		}
	}
}
