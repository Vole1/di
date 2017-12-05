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

		public void Visualize(string imageName)
		{
			var bmp = GetImage();
			bmp.Save(imageName + "." + ImageConfig.ImageFormat.ToString().ToLower(), ImageConfig.ImageFormat);
		}

		public Bitmap GetImage()
		{
			var bmp = new Bitmap(ImageConfig.ImageSize.Width, ImageConfig.ImageSize.Height);
			var drawingGraphics = Graphics.FromImage(bmp);
			drawingGraphics.Clear(ImageConfig.BackGroundColor);

			var rectangles = CircularCloudLayouter.PutRectangles();
			var words = CircularCloudLayouter.GetWords();

			var i = 0;
			foreach (var rectangle in rectangles)
			{
				var word = words[i];
				drawingGraphics.DrawString(word.Value, word.Font, word.Brush, rectangle);
				i++;
			}
			return bmp;
		}
	}
}
