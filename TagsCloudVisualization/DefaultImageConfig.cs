using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
	class DefaultImageConfig : IImageConfig
	{
		public Brush GetWordBrush(string word)
		{
			return new SolidBrush(Color.Chartreuse);
		}

		public Color BackGroundColor => Color.Black;

		public Size ImageSize => new Size(1000, 800);

		public Font WordsFont => new Font(FontFamily.GenericMonospace, 30);
		public float MaxFontSize => 10f;
		public float MinFontSize => 20f;

		public ImageFormat ImageFormat => ImageFormat.Bmp;

		public Point CloudCenter => new Point(ImageSize.Width / 2, ImageSize.Height / 2);
	}
}