using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class Program
	{
		public static void Main()
		{
			GenerateInput();
			var container = new ContainerConstructor();
			container.Run("in.txt");
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
