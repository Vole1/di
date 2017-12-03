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
		private ICircularCloudLayouter CircularCloudLayouter { get; }
		private IImageConfig ImageConfig { get; }

		public Visualizer(IImageConfig imageConfig, ICircularCloudLayouter circularCloudLayouter)
		{
			ImageConfig = imageConfig;
			CircularCloudLayouter = circularCloudLayouter;
		}

		public void Visualize()
		{
			var bmp = new Bitmap(ImageConfig.ImageSize.Width, ImageConfig.ImageSize.Height);
			var drawingGraphics = Graphics.FromImage(bmp);
			drawingGraphics.Clear(ImageConfig.BackGroundColor);

			var rectangles = CircularCloudLayouter.PutRectangles();
			var words = CircularCloudLayouter.GetWords();

			var i = 0;
			foreach (var rectangle in rectangles)
			{
				var rectangle1 = new Rectangle(rectangle.Location, new Size(rectangle.Width+20, rectangle.Height));
				var word = words[i];
				float fontSize = ImageConfig.MinAndMaxFonts[1] -
				                 (ImageConfig.MinAndMaxFonts[1] - ImageConfig.MinAndMaxFonts[0]) / (words.Length) * i;
				var currentFont = new Font(ImageConfig.WordsFont.FontFamily, fontSize);
				drawingGraphics.DrawString(words[i], currentFont, ImageConfig.GetWordBrush(word), rectangle);

				i++;
			}
			bmp.Save("picture3.bmp", ImageConfig.ImageFormat);
		}
	}
}
