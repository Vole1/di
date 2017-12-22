using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class Visualizer : IVisualizer
	{
		private IImageConfig ImageConfig { get; }

		public Visualizer(IImageConfig imageConfig)
		{
			ImageConfig = imageConfig;
		}

		public void Visualize(IEnumerable<Word> words, IEnumerable<Rectangle> rectangles, string imageName)
		{
			var bmp = GetImage(words, rectangles);
			bmp.Save(imageName + "." + ImageConfig.ImageFormat.ToString().ToLower(), ImageConfig.ImageFormat);
		}

		public Bitmap GetImage(IEnumerable<Word> words, IEnumerable<Rectangle> rectangles)
		{
			var bmp = new Bitmap(ImageConfig.ImageSize.Width, ImageConfig.ImageSize.Height);
			var drawingGraphics = Graphics.FromImage(bmp);
			drawingGraphics.Clear(ImageConfig.BackgroundColor);

			foreach (var wordAndRectangle in words.Zip(rectangles, (word, rectangle) => new {word, rectangle}))
			{
				var word = wordAndRectangle.word;
				var rectangle = wordAndRectangle.rectangle;
				drawingGraphics.DrawString(word.Value, word.Font, word.Brush, rectangle);
			}
			return bmp;
		}
	}
}
