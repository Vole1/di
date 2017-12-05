using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NHunspell;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class Program
	{
		public static void Main()
		{
			GenerateInput();
			var containerConstructor = new ContainerConstructor();
			containerConstructor.Contruct("in.txt");
			containerConstructor.SaveImage("img1");

		}

		private static void GenerateInput()
		{
			using (var streamWriter = new StreamWriter("in.txt"))
			{
				var words = new[] {"Hello", "my", "Dear", "Friend", "How", "Are", "you",
					"what", "can", "you", "tell", "me", "about", "yourself" };

				foreach (var word in words)
					streamWriter.WriteLineAsync(word);
			}
		}
	}
}
