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
			GenerateConfigInput();
			
			var container = new ContainerConstructor();
			container.Run("in.txt", "config.txt");
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

		private static void GenerateConfigInput()
		{
			using (var streamWriter = new StreamWriter("config.txt"))
			{
				var lines = new[]
				{
					"Brush color : Red",
					"Background color : Black",
					"Image size : (900, 900)",
					"Words font : \"Arial\"",
					"Font sizes : (10, 20)",
					"Image format : png"
				};

				foreach (var line in lines)
					streamWriter.WriteLineAsync(line);
			}
		}
	}
}
